using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public Bullet bulletPrefab;
	public Transform bulletOrigin;
	public bool isAutomatic = false;
	public float fireRate = 0.2f;
	public float initialBulletFireSpeed = 100;
	public float initialBulletRotationSpeed = 500;
	public float spreadAngle = 0;

	private float timeOfLastFire = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Fire() {
		Bullet newBullet = (Bullet)Instantiate(bulletPrefab, bulletOrigin.position, transform.rotation);
		newBullet.rootTransformOfOrigin = transform.root;
		Vector3 bulletDirection = Quaternion.Euler(0, 0, Random.Range(-spreadAngle / 2f, spreadAngle / 2f)) * transform.right;
		newBullet.rigidbody.AddForce(bulletDirection * initialBulletFireSpeed);
		newBullet.rigidbody.AddTorque(transform.forward * initialBulletRotationSpeed);
		timeOfLastFire = Time.time;
	}

	public bool CanFire() {
		return Time.time - timeOfLastFire > fireRate;
	}
}
