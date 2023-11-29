using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBullet : MonoBehaviour {

	public float speed = 15f;
	public float range = 15f;
	public GameObject particle;
	Rigidbody2D rb;

	Transform target;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		FindTarget();
		if(target == null)
		{
			Destroy(gameObject);
			return;
		}
		if(target.position.y >= transform.position.y)
		{
			transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(target.position, transform.position));
		}else if(target.position.y < transform.position.y)
		{
			transform.rotation = Quaternion.Euler(0, 0, -Vector2.Angle(target.position, transform.position));
		}
		
		Vector2 direction = target.position - transform.position;
		rb.velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			if(enemy != null)
			{
				enemy.TakeDamage(gameObject.GetComponentInParent<Soldier>().damage);
			}
			if (particle != null)
			{
				GameObject part = Instantiate(particle, gameObject.transform.position, gameObject.transform.rotation);
				Destroy(part, 1);
			}
			Destroy(gameObject);
		}
	}

	void FindTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float closest = Mathf.Infinity;
		GameObject nearest = null;

		foreach (GameObject enemy in enemies)
		{
			float distance = Vector2.Distance(transform.position, enemy.transform.position);
			if (distance < closest)
			{
				closest = distance;
				nearest = enemy;
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
