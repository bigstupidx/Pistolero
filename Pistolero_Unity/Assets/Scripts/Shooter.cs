using UnityEngine;
using System.Collections;
using System;

public class Shooter : MonoBehaviour {
	public Transform gunHolder;
	public Transform shieldHolder;

	public bool isReloading {get; private set;}
	public bool isAutoFiring {get; private set;}
	public float reloadProgress {get; private set;}

	public Action SignalReloadStarted;
	public Action SignalReloadFinished;
	public Action SignalReloadStopped;

	// might be connected and disconnected throughout the game
	public Gun gun {get {return _gun;}}
	public Shield shield {get {return _shield;}}

	public Entity entity {
		get {
			if (_entity == null) _entity = GetComponentInParent<Entity>();
			return _entity;
		}
	}

	[SerializeField]
	private Gun _gun;
	[SerializeField]
	private Shield _shield;

	private Entity _entity;

	void Awake () {
		isReloading = false;
		reloadProgress = 0;
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
		_shield.transform.parent = shieldHolder.transform;
		_shield.transform.localPosition = Vector3.zero;
		_shield.transform.localRotation = Quaternion.identity;
		_shield.TurnOff(0);
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
		reloadProgress = 0;
		isReloading = true;

		float timeOfReloadStart = Time.time;

		while (reloadProgress < 1) {
			reloadProgress = (Time.time - timeOfReloadStart) / gun.reloadTime;

			yield return null;
		}

		gun.Reload();

		isReloading = false;

		if (SignalReloadFinished != null) SignalReloadFinished();
	}

	public void TurnOnShield() {
		if (isReloading) StopReloading();
		if (isAutoFiring) StopAutoFiring();

		shield.TurnOn(0.1f, (tween) => {
			entity.entityCollider.enabled = false;
		});
	}

	public void TurnOffShield() {
		shield.TurnOff(0.1f, (tween) => {
			entity.entityCollider.enabled = true;
		});
	}

	public void StartReloading() {
		if (isReloading) Debug.LogWarning("trying to reload while already reloading");

		StartCoroutine("ReloadCoroutine");
		if (SignalReloadStarted != null) SignalReloadStarted();
	}

	public void StopReloading() {
		StopCoroutine("ReloadCoroutine");
		if (SignalReloadStopped != null) SignalReloadStopped();
		isReloading = false;
		reloadProgress = 0;
	}

	public void Fire(bool withScreenShake = false) {
		if (isReloading) StopReloading();
		gun.FireBullet(withScreenShake);
	}

	IEnumerator AutoFire() {
		while (gun.HasBulletsLeft()) {
			if (CanFire()) Fire(true);
			yield return null;
		}
	}

	public void StartAutoFiring() {
		isAutoFiring = true;
		StartCoroutine("AutoFire");
	}

	public void StopAutoFiring() {
		StopCoroutine("AutoFire");
		isAutoFiring = false;
	}

	public bool CanFire() {
		bool s = true;
		if (shield) s = !shield.isOn;

		bool t = gun.EnoughTimeHasLapsedForNextFire();

		bool r = gun.HasBulletsLeft();

		return s && t && r;
	}

	public bool CanReload() {
		return !isReloading && !gun.HasBulletsLeft();
	}
}
