﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableBody))]
public class Shooter : MonoBehaviour {
	public Transform shieldTransformRaised;
	public Transform shieldTransformLowered;
	public Transform gunHolder;

	// might be connected and disconnected throughout the game
	public Gun gun {get {return _gun;}}
	public Shield shield {get {return _shield;}}
	
	// connected from the beginning
	public DamageableBody damageableBody {get; private set;}

	[SerializeField]
	private Gun _gun;
	[SerializeField]
	private Shield _shield;

	void Awake () {
		damageableBody = GetComponent<DamageableBody>();
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
		_shield.Lower();
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
}
