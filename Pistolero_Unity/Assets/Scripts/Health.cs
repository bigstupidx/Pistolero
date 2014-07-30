using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour {
	public float hp = 500;

	public Shield shield {
		get {
			if (!_shield) {
				_shield = GetComponentInChildren<Shield>();
			}
			return _shield;
		}
	}

	private Shield _shield = null;
	private Death death;

	// Use this for initialization
	void Start () {
		death = GetComponent<Death>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnTriggerEnter(Collider coll) {
//		Bullet bullet = coll.GetComponent<Bullet>();
//
//		if (bullet) {
//			// if the shield is raised, the bullets shouldn't hit the body directly at all,
//			// but this will double check that to make sure only the shield handles the damage
//			if (!shield.isRaised) Damage(bullet.damage);
//		}
//	}

	public void Damage(float damageAmount) {
		hp = Mathf.Max(0, hp - damageAmount);
		if (hp <= 0) {
			if (!death.isDead) death.Die();
		}
	}
}
