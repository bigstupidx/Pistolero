using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float damage = 50;
	
	[HideInInspector] public Transform rootTransformOfOrigin;

	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find("Bullet Holder").transform;
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

		Entity entity = coll.transform.root.GetComponentInChildren<Entity>();

		if (entity) {
			if (entity.shield.isOn) {
				entity.health.Damage(damage * entity.shield.damageMultiplier);
			}
			else {
				entity.health.Damage(damage);
			}

			Kill();
			//Debug.Log("kill " + rootTransformOfOrigin.name + " bullet");
		}
	}

	void Kill() {
		Destroy(this.gameObject);
	}
}
