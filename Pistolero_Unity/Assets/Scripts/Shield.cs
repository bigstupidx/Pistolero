using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public float damageMultiplier = 0.3f;
	public bool isRaised {get; private set;}

	public Transform transformRaised;
	public Transform transformLowered;

	private DamageableBody damageableBody;

	// Use this for initialization
	void Start () {
		isRaised = false;
		damageableBody = GetComponentInParent<DamageableBody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Raise(float time = 0) {
		isRaised = true;

		Go.killAllTweensWithTarget(transform);

		transform.parent = transformRaised;

		if (time == 0) {
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}
		else {
			Go.to(transform, time, new GoTweenConfig().localPosition(Vector3.zero).localRotation(Quaternion.identity));
		}
	}

	public void Lower(float time = 0) {
		isRaised = false;

		Go.killAllTweensWithTarget(transform);

		transform.parent = transformLowered;

		if (time == 0) {
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}
		else {
			Go.to(transform, time, new GoTweenConfig().localPosition(Vector3.zero).localRotation(Quaternion.identity));
		}
	}

	void OnTriggerEnter(Collider coll) {
		Bullet bullet = coll.GetComponent<Bullet>();

		if (bullet) {
			// don't collide with the person who shot the bullet
			if (bullet.rootTransformOfOrigin == transform.root) return;

			// if it's not raised, the bullet shouldn't hit the shield at all
			if (isRaised) damageableBody.Damage(bullet.damage * damageMultiplier);
		}
	}
}
