using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
	public GameObject shootParticle;

	public string[] sounds;

	Enemy enemy;
	bool reloaded = true;

	// Use this for initialization
	void Start()
	{
		enemy = gameObject.GetComponent<Enemy>();
	}

	// Update is called once per frame
	void Update()
	{
		if (enemy.inPlace && reloaded && GameObject.FindGameObjectsWithTag("Soldier").Length != 0)
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
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
		bullet.transform.parent = gameObject.transform;
		Destroy(bullet, 3f);
		yield return new WaitForSeconds(enemy.shootSpeed);
		reloaded = true;
		yield return null;
	}
}
