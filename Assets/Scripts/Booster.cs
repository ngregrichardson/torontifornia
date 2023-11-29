using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {

	public Sprite[] sprites;
	float morale;

	// Use this for initialization
	void Start () {
		morale = Random.Range(5, 30);
		gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
	}

	void OnMouseEnter()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.green;
	}

	void OnMouseExit()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.white;
	}

	void OnMouseDown()
	{
		SoldierManager.instance.RaiseMorale(morale);
		AudioManager.instance.Play("Booster");
		Destroy(gameObject);
	}
}
