using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public GameObject solider;
	public GameObject countdownText;

	float remaining;
	bool buying = false;

	void Start()
	{
		remaining = solider.GetComponent<Soldier>().cooldown + 1;
	}

	void Update () {
		if(buying)
		{
			if(remaining > 1.5)
			{
				remaining -= Time.deltaTime;
				countdownText.GetComponent<Text>().text = Mathf.FloorToInt(remaining).ToString();
			}else
			{
				ResetVars();
			}
		}
	}

	public void SetBuying()
	{
		buying = true;
	}

	void ResetVars()
	{
		remaining = solider.GetComponent<Soldier>().cooldown + 1;
		buying = false;
	}
}
