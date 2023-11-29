using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	#region Singleton
	public static GameManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	public void Win()
	{
		SceneManager.LoadScene(3);
	}

	public void Lose()
	{
		AudioManager.instance.Play("Lose");
		SceneManager.LoadScene(4);
	}

	public void Cellar()
	{
		AudioManager.instance.Stop("Theme");
		AudioManager.instance.Play("Growl");
		SceneManager.LoadScene(5);
	}
}
