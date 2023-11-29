using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	private void Start()
	{
		AudioManager.instance.Play("Theme");
	}

	public void StartButton()
	{
		AudioManager.instance.Play("Click");
		SceneManager.LoadScene(1);
	}

	public void InstructionsButton()
	{
		AudioManager.instance.Play("Click");
		SceneManager.LoadScene(2);
	}

	public void QuitButton()
	{
		AudioManager.instance.Play("Click");
		Application.Quit();
	}
}
