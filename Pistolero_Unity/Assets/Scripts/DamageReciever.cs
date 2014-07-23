using UnityEngine;
using System.Collections;

public class DamageReciever : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		Bullet bullet = coll.GetComponent<Bullet>();

		if (bullet != null) {
			Debug.Log(bullet.damage + " damage!");
		}
	}
}
