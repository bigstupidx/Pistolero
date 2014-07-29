using UnityEngine;
using System.Collections;

public class DamageableBody : MonoBehaviour {
	public Shield shield {
		get {
			if (!_shield) {
				_shield = GetComponentInChildren<Shield>();
			}
			return _shield;
		}
	}

	private Shield _shield = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		Bullet bullet = coll.GetComponent<Bullet>();

		if (bullet) {
			// if the shield is raised, the bullets shouldn't hit the body directly at all,
			// but this will double check that to make sure only the shield handles the damage
			if (!shield.isRaised) Damage(bullet.damage);
		}
	}

	public void Damage(float damageAmount) {
		Debug.Log(damageAmount + " damage!");
	}
}
