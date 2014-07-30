﻿using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Transform shieldTransformRaised;
	public Transform shieldTransformLowered;
	public Transform gunHolder;

	public bool isReloading {get; private set;}

	// might be connected and disconnected throughout the game
	public Gun gun {get {return _gun;}}
	public Shield shield {get {return _shield;}}

	[SerializeField]
	private Gun _gun;
	[SerializeField]
	private Shield _shield;

	void Awake () {
		isReloading = false;
	}

	public void AddGun(Gun newGun) {
		if (gun) throw new UnityException("can't add gun when gun already exists");

		Quaternion localRotation = newGun.transform.localRotation;

		_gun = newGun;
		_gun.transform.parent = gunHolder.transform;
		_gun.transform.localPosition = Vector3.zero;
		_gun.transform.localRotation = localRotation;
	}
	
	public void AddShield(Shield newShield) {
		if (shield) throw new UnityException("can't add shield when shield already exists");

		_shield = newShield;
		_shield.transformLowered = shieldTransformLowered;
		_shield.transformRaised = shieldTransformRaised;
		_shield.MoveToLoweredPosition(0);
	}
	
	public void RemoveGun() {
		if (!gun) throw new UnityException("can't remove gun when gun doesn't exist");

		if (!Application.isPlaying) DestroyImmediate(gun.gameObject);
		else Destroy(gun.gameObject);

		_gun = null;
	}
	
	public void RemoveShield() {
		if (!shield) throw new UnityException("can't remove shield when shield doesn't exist");

		if (!Application.isPlaying) DestroyImmediate(shield.gameObject);
		else Destroy(shield.gameObject);

		_shield = null;
	}

	IEnumerator ReloadCoroutine() {
		isReloading = true;

		shield.MoveToLoweredPosition();

		while (gun.bulletsLeft < gun.bulletCount) {
			yield return new WaitForSeconds(gun.reloadTimePerBullet);

			gun.bulletsLeft++;
		}
		
		isReloading = false;
	}

	public void RaiseShield() {
		if (isReloading) CancelReload();
		shield.MoveToRaisedPosition();
	}

	public void LowerShield() {
		shield.MoveToLoweredPosition();
	}

	public void Reload() {
		if (isReloading) Debug.LogWarning("trying to reload while already reloading");

		StartCoroutine("ReloadCoroutine");
	}

	public void CancelReload() {
		StopCoroutine("ReloadCoroutine");

		isReloading = false;
	}

	public void Fire() {
		if (isReloading) CancelReload();
		gun.FireBullet();
	}

	public bool CanFire() {
		bool s = true;
		if (shield) s = !shield.isRaised;

		bool t = gun.EnoughTimeHasLapsedForNextFire();

		bool r = gun.HasBulletsLeft();

		return s && t && r;
	}

	public bool CanReload() {
		return !isReloading && !gun.HasBulletsLeft();
	}
}
