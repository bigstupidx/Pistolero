using UnityEngine;
using System.Collections;
using System;

public class Shield : MonoBehaviour {
	public float damageMultiplier = 0.3f;
	public bool isOn {get; private set;}

	public CapsuleCollider shieldCollider;

	private tk2dSprite sprite;
	
	// Use this for initialization
	void Awake () {
		isOn = false;
		sprite = GetComponent<tk2dSprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TurnOn(float time = 0.1f, Action<AbstractGoTween> onComplete = null) {
		if (isOn && time != 0) Debug.LogWarning("trying to raise shield while already raised");

		Go.killAllTweensWithTarget(sprite);

		Action<AbstractGoTween> internalOnComplete = (tween) => {
			shieldCollider.enabled = true;
			isOn = true;
			if (onComplete != null) onComplete(tween);
		};

		Color newColor = sprite.color;

		if (time == 0) {
			newColor.a = 1;
			sprite.color = newColor;
			internalOnComplete(null);
		}
		else {
			newColor.a = 1;
			GoTweenConfig config = new GoTweenConfig().colorProp("color", newColor).onComplete(internalOnComplete);

			Go.to(sprite, time, config);
		}
	}

	public void TurnOff(float time = 0.1f, Action<AbstractGoTween> onComplete = null) {
		if (!isOn && time != 0) Debug.LogWarning("trying to lower shield while already lowered");

		Go.killAllTweensWithTarget(sprite);

		Action<AbstractGoTween> internalOnComplete = (tween) => {
			shieldCollider.enabled = false;
			isOn = false;
			if (onComplete != null) onComplete(tween);
		};

		Color newColor = sprite.color;

		if (time == 0) {
			newColor.a = 0;
			sprite.color = newColor;
			internalOnComplete(null);
		}
		else {
			newColor.a = 0;
			GoTweenConfig config = new GoTweenConfig().colorProp("color", newColor).onComplete(internalOnComplete);

			Go.to(sprite, time, config);
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
