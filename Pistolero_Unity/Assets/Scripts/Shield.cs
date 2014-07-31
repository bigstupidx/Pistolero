using UnityEngine;
using System.Collections;
using System;

public class Shield : MonoBehaviour {
	public float damageMultiplier = 0.3f;
	public bool isRaised {get; private set;}

	public Transform transformRaised;
	public Transform transformLowered;

	public CapsuleCollider shieldCollider;
	
	// Use this for initialization
	void Start () {
		isRaised = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveToRaisedPosition(float time = 0.1f, Action<AbstractGoTween> onComplete = null) {
		if (isRaised && time != 0) Debug.LogWarning("trying to raise shield while already raised");

		Go.killAllTweensWithTarget(transform);

		transform.parent = transformRaised;

		Action<AbstractGoTween> internalOnComplete = (tween) => {
			shieldCollider.enabled = true;
			isRaised = true;
			if (onComplete != null) onComplete(tween);
		};
		
		if (time == 0) {
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			internalOnComplete(null);
		}
		else {
			GoTweenConfig config = new GoTweenConfig().localPosition(Vector3.zero).localRotation(Quaternion.identity).onComplete(internalOnComplete);

			Go.to(transform, time, config);
		}
	}

	public void MoveToLoweredPosition(float time = 0.1f, Action<AbstractGoTween> onComplete = null) {
		if (!isRaised && time != 0) Debug.LogWarning("trying to lower shield while already lowered");

		Go.killAllTweensWithTarget(transform);

		transform.parent = transformLowered;

		Action<AbstractGoTween> internalOnComplete = (tween) => {
			shieldCollider.enabled = false;
			isRaised = false;
			if (onComplete != null) onComplete(tween);
		};

		if (time == 0) {
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			internalOnComplete(null);
		}
		else {
			GoTweenConfig config = new GoTweenConfig().localPosition(Vector3.zero).localRotation(Quaternion.identity).onComplete(internalOnComplete);

			Go.to(transform, time, config);
		}
	}

	void OnTriggerEnter(Collider coll) {

//		Bullet bullet = coll.GetComponent<Bullet>();
//
//		if (bullet) {
//			// don't collide with the person who shot the bullet
//			if (bullet.rootTransformOfOrigin == transform.root) return;
//
//			// if it's not raised, the bullet shouldn't hit the shield at all
//			if (isRaised) health.Damage(bullet.damage * damageMultiplier);
//		}
	}
}
