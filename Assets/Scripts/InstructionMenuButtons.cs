using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionMenuButtons : MonoBehaviour {

	public void BackButton()
	{
		AudioManager.instance.Play("Click");
		SceneManager.LoadScene(0);
	}
}
