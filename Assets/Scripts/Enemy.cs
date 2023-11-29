using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int level = 0;
	public int health = 100;
	public int moveSpeed = 3;
	public int shootSpeed = 3;
	public int damage = 10;
	public int weight = 1;
	public float morale = 3;
	public GameObject ap;

	public bool inPlace = false;

	Rigidbody2D rb;
	Animator anim;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (transform.position.x > ap.transform.position.x)
		{
			rb.velocity = new Vector2(-moveSpeed, 0);
		}
		else
		{
			if (!IsInFrustum(gameObject.GetComponent<SpriteRenderer>(), Camera.main))
			{
				ap.transform.Translate(new Vector2(-1, 0));
			}
			else
			{
				rb.velocity = Vector2.zero;
				inPlace = true;
			}
		}
		anim.SetBool("walking", !inPlace);
	}

	public void TakeDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		SoldierManager.instance.RaiseMorale(morale);
		Destroy(gameObject);
	}

	private bool IsInFrustum(Renderer renderer, Camera cam)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

		foreach (Plane plane in planes)
		{
			if(plane.GetDistanceToPoint(transform.TransformPoint(renderer.bounds.max)) < 0)
			{
				return false;
			}
		}

		return true;
	}
}
