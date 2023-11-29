using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

	#region Singleton
	public static MenuManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	public List<GameObject> soldierPrefab;
	public List<GameObject> spawnPoints;
	public List<GameObject> countdowns;

	GameObject soldier;

	public void Buy(int level)
	{
		AudioManager.instance.Play("Click");
		StartCoroutine(Buying(level));
	}

	IEnumerator Buying(int level)
	{
		GameObject button = EventSystem.current.currentSelectedGameObject;
		Vector2 position = new Vector2(spawnPoints[0].transform.position.x, Random.Range(spawnPoints[1].transform.position.y, spawnPoints[0].transform.position.y));
		soldier = Instantiate(soldierPrefab[level], position, Quaternion.identity);
		if (SoldierManager.instance.totalWeights + soldier.GetComponent<Soldier>().weight > 10)
		{
			button.GetComponent<CanvasRenderer>().SetColor(new Color(255, 0, 0, 1));
			Destroy(soldier);
			yield return new WaitForSeconds(0.3f);
			button.GetComponent<CanvasRenderer>().SetColor(Color.white);
			yield break;
		}
		SoldierManager.instance.AddSoldier(soldier);
		button.GetComponent<Button>().interactable = false;
		button.GetComponent<CanvasRenderer>().SetColor(Color.gray);
		countdowns[level].SetActive(true);
		yield return new WaitForSeconds(soldier.GetComponent<Soldier>().cooldown);
		button.GetComponent<Button>().interactable = true;
		button.GetComponent<CanvasRenderer>().SetColor(Color.white);
		countdowns[level].SetActive(false);
	}
}
