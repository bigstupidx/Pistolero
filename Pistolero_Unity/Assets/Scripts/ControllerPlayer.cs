using UnityEngine;
using System.Collections;

public class ControllerPlayer : MonoBehaviour {
	private Shooter shooter;
	private TouchDispatcher touchDispatcher;

	void Awake() {
		touchDispatcher = GameObject.Find("Touch Dispatcher").GetComponent<TouchDispatcher>();
	}

	void Start() {
		shooter = GetComponentInChildren<Shooter>();
	}

	void OnEnable() {
		touchDispatcher.SignalTouch += HandleTouch;
	}

	void OnDisable() {
		touchDispatcher.SignalTouch -= HandleTouch;
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.S)) {
			shooter.TurnOnShield();
		}
		else if (Input.GetKeyUp(KeyCode.S)) {
			shooter.TurnOffShield();
		}
	}

	void HandleTouch(TouchSide touchSide, TouchType touchType) {
		if (touchSide == TouchSide.Left) {
			if (touchType == TouchType.OnDown) shooter.TurnOnShield();
			else if (touchType == TouchType.OnRelease) shooter.TurnOffShield();
		}

		else if (touchSide == TouchSide.Right) {
			if (touchType == TouchType.OnDown) {
				if (!shooter.shield.isOn) {
					if (shooter.CanFire()) {
						if (shooter.gun.isAutomatic) shooter.StartAutoFiring();
						else shooter.Fire(true);
					}
					else if (shooter.CanReload()) shooter.Reload();
				}
			}
			else if (touchType == TouchType.OnRelease) {
				if (shooter.isAutoFiring) shooter.StopAutoFiring();
			}
		}
	}

	void HandleRightTouch(TouchPhase phase) {

	}

	void HandleLeftTouch(TouchPhase phase) {

	}
}
