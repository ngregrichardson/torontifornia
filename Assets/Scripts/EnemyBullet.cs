using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	public float speed = 15f;
	public float range = 14.5f;
	Rigidbody2D rb;

	Transform target;

	// Use this for initialization
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		FindTarget();
		if (target == null)
		{
			return;
		}
		Vector2 direction = target.position - transform.position;
		rb.velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Soldier")
		{
			Soldier soldier = collision.gameObject.GetComponent<Soldier>();
			if (soldier != null)
			{
				soldier.TakeDamage(gameObject.GetComponentInParent<Enemy>().damage);
			}
			Destroy(gameObject);
		}
	}

	void FindTarget()
	{
		GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Soldier");
		float closest = Mathf.Infinity;
		GameObject nearest = null;

		foreach (GameObject soldier in soldiers)
		{
			float distance = Vector2.Distance(transform.position, soldier.transform.position);
			if(distance >= range)
			{
				Destroy(gameObject);
				return;
			}
			if (distance < closest)
			{
				closest = distance;
				nearest = soldier;
			}
		}

		if (nearest != null)
		{
			target = nearest.transform;
		}
		else
		{
			nearest = null;
		}
	}
}
