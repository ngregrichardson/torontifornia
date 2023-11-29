using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	#region Singleton
	public static WaveManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public GameObject enemy;
		public int count;
		public float rate;
		public float moraleBooster;
	}

	public GameObject booster;

	public GameObject[] fakeButtons;

	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	public SpawnState state = SpawnState.COUNTING;

	public Text waveNumberText;
	public GameObject waveCompletedText;

	// Use this for initialization
	void Start () {
		waveCountdown = timeBetweenWaves;
	}
	
	// Update is called once per frame
	void Update () {
		if(state == SpawnState.WAITING)
		{
			if(EnemyIsAlive())
			{
				return;
			}
			WaveCompleted();
		}
		if(waveCountdown <= 0)
		{
			if(state != SpawnState.SPAWNING)
			{
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		for(int i = 1; i < 9; i++)
		{
			AudioManager.instance.Stop("Grunt " + i);
		}
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		waveCompletedText.SetActive(true);

		AudioManager.instance.Play("Cheer");

		if (nextWave + 1 > waves.Length -1)
		{
			GameManager.instance.Win();
		}else
		{
			nextWave++;
			SoldierManager.instance.RaiseMorale(waves[nextWave - 1].moraleBooster);
			GameObject boost = Instantiate(booster, new Vector2(Random.Range(MenuManager.instance.spawnPoints[4].transform.position.x, MenuManager.instance.spawnPoints[5].transform.position.x), Random.Range(MenuManager.instance.spawnPoints[4].transform.position.y, MenuManager.instance.spawnPoints[5].transform.position.y)), Quaternion.identity);
			Destroy(boost, 10);

			if(nextWave >= 2 && nextWave < 4)
			{
				fakeButtons[0].SetActive(false);
			}else if(nextWave >= 4 && nextWave < 6)
			{
				fakeButtons[1].SetActive(false);
			}
			else if(nextWave >= 6 && nextWave < 8)
			{
				fakeButtons[2].SetActive(false);
			}
			else if(nextWave >= 8 && nextWave < 10)
			{
				fakeButtons[3].SetActive(false);
			}
			else if(nextWave >= 10 && nextWave < 12)
			{
				fakeButtons[4].SetActive(false);
			}
		}
		waveNumberText.text = " Wave " + nextWave.ToString();
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		waveCompletedText.SetActive(false);
		state = SpawnState.SPAWNING;

		for(int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f / _wave.rate);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(GameObject _enemy)
	{
		Instantiate(_enemy, new Vector2(MenuManager.instance.spawnPoints[2].transform.position.x, Random.Range(MenuManager.instance.spawnPoints[3].transform.position.y, MenuManager.instance.spawnPoints[2].transform.position.y)), Quaternion.identity);
	}
}
