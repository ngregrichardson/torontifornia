using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {

	public Text health;
	public Text speed;
	public Text damage;
	public Text weight;
	public int level;

	List<GameObject> soldiers;

	void Start()
	{
		soldiers = MenuManager.instance.soldierPrefab;
		foreach(GameObject go in soldiers)
		{
			Soldier soldier = go.GetComponent<Soldier>();
			if (soldier.level == level)
			{
				health.text = soldier.health.ToString();
				speed.text = soldier.moveSpeed.ToString();
				damage.text = soldier.damage.ToString();
				weight.text = soldier.weight.ToString();
			}
		}
	}

	public void OpenDescription(GameObject description)
	{
		description.SetActive(true);
	}

	public void CloseDescription(GameObject description)
	{
		description.SetActive(false);
	}
}
