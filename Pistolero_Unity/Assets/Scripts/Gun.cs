using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public Bullet bulletPrefab;
	public Transform bulletOrigin;
	public bool isAutomatic = false;
	public int bulletCount = 6;
	public int bulletsLeft;
	public float reloadTimePerBullet = 0.1f;
	public float fireRate = 0.2f;
	public float fireForce = 100;
	public float fireForceVariation = 0;
	public float rotationSpeed = 500;
	public float spreadAngle = 0;
	public float shakeIntensity = 1;
	public float shakeTime = 0.2f;

	private float timeOfLastFire = 0;
	private float aspectRatio;

	// Use this for initialization
	void Awake () {
		aspectRatio = (float)Screen.width / (float)Screen.height;
		bulletsLeft = bulletCount;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void FireBullet(bool withScreenShake = false) {
		if (!HasBulletsLeft()) Debug.LogWarning("can't fire; out of bullets!");

		Bullet newBullet = (Bullet)Instantiate(bulletPrefab, bulletOrigin.position, transform.rotation);
		newBullet.rootTransformOfOrigin = transform.root;

		Vector3 bulletDirection = Quaternion.Euler(0, 0, Random.Range(-spreadAngle / 2f, spreadAngle / 2f)) * transform.right;
		float fireSpeedVariation = Random.Range(0, fireForceVariation);
		newBullet.rigidbody.AddForce(bulletDirection * (fireForce + fireSpeedVariation) * aspectRatio);
		newBullet.rigidbody.AddTorque(transform.forward * rotationSpeed);
		timeOfLastFire = Time.time;
		bulletsLeft--;

		if (withScreenShake) {
			CameraHelper.instance.gameCam.GetComponent<ScreenShake>().Shake(shakeIntensity, shakeTime);
		}
	}

	public bool HasBulletsLeft() {
		return bulletsLeft > 0;
	}

	public bool EnoughTimeHasLapsedForNextFire() {
		return (Time.time - timeOfLastFire) > fireRate;
	}
}
