using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoGoToMainMenu : MonoBehaviour {

	public GameObject countdown;

	float count = 5;

	void Update()
	{
		if(count > 0.5)
		{
			count -= Time.deltaTime;
			countdown.GetComponent<Text>().text = Mathf.Floor(count).ToString();
			return;
		}
		SceneManager.LoadScene(0);
	}
}
