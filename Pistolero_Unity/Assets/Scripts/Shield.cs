using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public float damageMultiplier = 0.3f;
	public bool isRaised {get; private set;}

	public Transform transformRaised;
	public Transform transformLowered;

	private Health health;

	// Use this for initialization
	void Start () {
		isRaised = false;
		health = GetComponentInParent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveToRaisedPosition(float time = 0.1f) {
		if (isRaised && time != 0) Debug.LogWarning("trying to raise shield while already raised");

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

	public void MoveToLoweredPosition(float time = 0.1f) {
		if (!isRaised && time != 0) Debug.LogWarning("trying to lower shield while already lowered");

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

//	void OnTriggerEnter(Collider coll) {
//		Bullet bullet = coll.GetComponent<Bullet>();
//
//		if (bullet) {
//			// don't collide with the person who shot the bullet
//			if (bullet.rootTransformOfOrigin == transform.root) return;
//
//			// if it's not raised, the bullet shouldn't hit the shield at all
//			if (isRaised) health.Damage(bullet.damage * damageMultiplier);
//		}
//	}
}
