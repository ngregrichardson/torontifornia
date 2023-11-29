using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeapon : MonoBehaviour
{

	public Transform firePoint;
	public GameObject bulletPrefab;
	public GameObject shootParticle;

	public string[] sounds;

	Animator anim;

	Soldier soldier;
	bool reloaded = true;

	// Use this for initialization
	void Start()
	{
		soldier = gameObject.GetComponent<Soldier>();
		anim = gameObject.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (soldier.inPlace && reloaded && GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
		{
			StartCoroutine(Shoot());
		}
	}

	IEnumerator Shoot()
	{
		reloaded = false;
		AudioManager.instance.Play(sounds[Mathf.FloorToInt(Random.Range(0, sounds.Length))]);
		if (shootParticle != null)
		{
			shootParticle.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			shootParticle.SetActive(false);
		}
		anim.SetTrigger("shooting");
	}

	IEnumerator SpawnBullet()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
		bullet.transform.parent = gameObject.transform;
		Destroy(bullet, 3f);
		yield return new WaitForSeconds(soldier.shootSpeed * SoldierManager.instance.shootDep);
		reloaded = true;
		yield return null;
	}
}
