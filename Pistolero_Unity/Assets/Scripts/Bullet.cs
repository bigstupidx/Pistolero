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
		Vector3 pos = transform.position;
		Rect r = Helper.instance.GetScreenRectInWorldSpace();

		if (pos.x < r.xMin ||
		    pos.x > r.xMax ||
		    pos.y < r.yMin ||
		    pos.y > r.yMax) {

			Kill();
		}
	}

	void OnTriggerEnter(Collider coll) {
		// don't collide with the person who shot the bullet
		if (coll.transform.root == rootTransformOfOrigin) return;

		Kill();
//		Shooter hitShooter = coll.GetComponentInChildren<Shooter>();
//	
//		if (hitShooter) Kill();
	}

	void Kill() {
		Destroy(this.gameObject);
	}
}
