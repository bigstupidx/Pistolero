using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float damage = 50;

	[HideInInspector] public Transform rootTransformOfOrigin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		// don't collide with the person who shot the bullet
		if (coll.transform.root == rootTransformOfOrigin) return;

		DamageableBody damageableBody = coll.GetComponent<DamageableBody>();
		Shield shield = coll.GetComponent<Shield>();

		if (damageableBody) {
			Kill();
		}

		if (shield && shield.isRaised) {
			Kill();
		}
	}

	void Kill() {
		Destroy(this.gameObject);
	}
}
