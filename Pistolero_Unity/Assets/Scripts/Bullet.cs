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
		Rect r = CameraHelper.instance.GetScreenRectInWorldSpace();

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

		Shield shield = coll.GetComponent<Shield>();
		Entity entity = coll.transform.root.GetComponent<Entity>();

		if (entity) {
			if (entity.shield.isRaised) {
				entity.health.Damage(damage * entity.shield.damageMultiplier);
			//	Debug.Log("shield up: " + entity.name);
			}
			else {
				entity.health.Damage(damage);
				//Debug.Log("shield down: " + entity.name);

			}

			Kill();
		}
	}

	void Kill() {
		Destroy(this.gameObject);
	}
}
