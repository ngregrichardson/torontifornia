using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour {

	public GameObject menu;

	public void OpenPauseMenu()
	{
		AudioManager.instance.Play("Click");
		Time.timeScale = 0;
		menu.SetActive(true);
	}

	public void ClosePauseMenu()
	{
		AudioManager.instance.Play("Click");
		Time.timeScale = 1;
		menu.SetActive(false);
	}

	public void MainMenuButton()
	{
		AudioManager.instance.Play("Click");
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}
