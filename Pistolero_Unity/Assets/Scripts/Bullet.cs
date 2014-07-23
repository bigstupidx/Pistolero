using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float damage = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		DamageReciever damageReciever = coll.GetComponent<DamageReciever>();

		if (damageReciever != null) {
			Kill();
		}
	}

	void Kill() {
		Destroy(this.gameObject);
	}
}
