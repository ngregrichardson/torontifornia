using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierManager : MonoBehaviour {

	public Text soldierCounter;

	public int totalWeights;
	public float morale;
	public float maxMorale = 100f;
	public GameObject moraleBar;
	public Text moraleBarText;
	public GameObject abandonedText;

	public Sprite morale100;
	public Sprite morale75;
	public Sprite morale50;
	public Sprite morale25;

	public float moveDep = 1;
	public float shootDep = 1;

	bool abandoned = false;
	public bool abandoning = false;

	#region Singleton
	public static SoldierManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	void Start()
	{
		morale = maxMorale;
	}

	public List<GameObject> soldiers;
	
	// Update is called once per frame
	void Update () {
		totalWeights = CalculateWeights();

		if(morale <= 75 && morale > 50)
		{
			moraleBar.GetComponent<Image>().sprite = morale75;
			moveDep = 0.5f;
			abandoned = false;
			Debug.Log("slowed moving");
		}
		else if(morale <= 50 && morale > 25)
		{
			moraleBar.GetComponent<Image>().sprite = morale50;
			shootDep = 1.5f;
			abandoned = false;
		}
		else if(morale <= 25 && morale > 0)
		{
			moraleBar.GetComponent<Image>().sprite = morale25;
			if(!abandoned)
			{
				StartCoroutine(Abandon());
				abandoned = true;
			}
		}
		else if(morale <= 0)
		{
			GameManager.instance.Lose();
		}else
		{
			moraleBar.GetComponent<Image>().sprite = morale100;
			moveDep = 1;
			shootDep = 1;
			abandoned = false;
		}
	}

	public List<GameObject> GetSoldiers()
	{
		return soldiers;
	}

	public void AddSoldier(GameObject soldier)
	{
		soldiers.Add(soldier);
		UpdateText();
	}

	public void RemoveSoldier(GameObject soldier)
	{
		soldiers.Remove(soldier);
		UpdateText();
	}

	int CalculateWeights()
	{
		int total = 0;
		foreach(GameObject soldier in soldiers)
		{
			total += soldier.GetComponent<Soldier>().weight;
		}
		return total;
	}

	void UpdateText()
	{
		soldierCounter.text = CalculateWeights() + "/10";
	}

	public void LowerMorale(float _morale)
	{
		morale -= _morale;
		UpdateMoraleBar();
	}

	public void RaiseMorale(float _morale)
	{
		if(morale + _morale > 100)
		{
			morale = 100;
		}else
		{
			morale += _morale;
		}
		UpdateMoraleBar();
	}

	void UpdateMoraleBar()
	{
		moraleBar.GetComponent<Image>().fillAmount = morale / maxMorale;
		moraleBarText.text = morale + " Morale";
	}

	IEnumerator Abandon()
	{
		abandonedText.SetActive(true);
		for (int i = 0; i < soldiers.Count; i++)
		{
				soldiers[i].GetComponent<SpriteRenderer>().flipX = true;
				soldiers[i].GetComponent<Soldier>().abandoning = true;
				Destroy(soldiers[i], 7f);
				soldiers.Remove(soldiers[i]);
				UpdateText();
		}
		yield return new WaitForSeconds(3f);
		abandonedText.SetActive(false);
	}
}
