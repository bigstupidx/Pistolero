using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
	bool isShaking = false;
	Vector3 originalPosition;

	void Start () {
		originalPosition = transform.position;
	}
	
	void Update () {
	
	}

	public void Shake(float intensity, float time) {
		if (isShaking) {
			Go.killAllTweensWithTarget(transform);
			transform.position = originalPosition;
		}

		isShaking = true;

		Go.to(transform, time, new GoTweenConfig()
		      .shake(new Vector3(intensity, intensity, 0), GoShakeType.Position)
		      .onComplete((tween) => {
			isShaking = false;
		}));
	}
}
