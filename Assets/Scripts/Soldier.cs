using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Soldier : MonoBehaviour {

	Animator anim;
	public int level = 0;
	public int health = 100;
	public int moveSpeed = 3;
	public float shootSpeed = 3;
	public int damage = 10;
	public int weight = 1;
	public int cooldown = 3;
	public float morale = 3;
	public GameObject ap;

	public bool inPlace = false;
	public bool abandoning = false;

	Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (!abandoning)
		{
			if (transform.position.x < ap.transform.position.x)
			{
				rb.velocity = new Vector2(moveSpeed * SoldierManager.instance.moveDep, 0);
				inPlace = false;
			}
			else
			{
				if (!IsInFrustum(gameObject.GetComponent<SpriteRenderer>(), Camera.main))
				{
					ap.transform.Translate(new Vector2(1, 0));
				}
				else
				{
					rb.velocity = Vector2.zero;
					inPlace = true;
				}
			}
			anim.SetBool("walking", !inPlace);
		}else
		{
			rb.velocity = new Vector2(-5, 0);
			anim.SetBool("walking", true);
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;

		AudioManager.instance.Play("Grunt " + Mathf.FloorToInt(Random.Range(1, 8)));
		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		RemoveSoldier();
		SoldierManager.instance.LowerMorale(morale);
		Destroy(gameObject);
	}

	private void OnMouseOver()
	{
		gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
	}

	private void OnMouseExit()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.white;
	}

	void OnMouseDown()
	{
		SoldierManager.instance.LowerMorale(morale * 2);
		RemoveSoldier();
		Destroy(gameObject);
	}

	void RemoveSoldier()
	{
		SoldierManager.instance.RemoveSoldier(gameObject);
	}

	private bool IsInFrustum(Renderer renderer, Camera cam)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

		foreach (Plane plane in planes)
		{
			if (plane.GetDistanceToPoint(transform.TransformPoint(renderer.bounds.min)) < 0)
			{
				return false;
			}
		}

		return true;
	}
}
