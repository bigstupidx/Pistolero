using UnityEngine;
using System.Collections;

public class ControllerPlayer : MonoBehaviour {
	private Shooter shooter;
	private TouchDispatcher touchDispatcher;
	private EntityUIConnection entityUIConnection;

	void Awake() {
		touchDispatcher = GameObject.Find("Touch Dispatcher").GetComponent<TouchDispatcher>();
		entityUIConnection = GetComponent<EntityUIConnection>();
		entityUIConnection.reloadProgressBar.Hide();
		shooter = GetComponentInChildren<Shooter>();
		shooter.shield.TurnOff(0);
	}

	void OnEnable() {
		touchDispatcher.SignalTouch += HandleTouch;

		shooter.SignalReloadStarted += HandleReloadStarted;
		shooter.SignalReloadStopped += HandleReloadStopped;
		shooter.SignalReloadFinished += HandleReloadFinished;
	}

	void OnDisable() {
		touchDispatcher.SignalTouch -= HandleTouch;

		shooter.SignalReloadStarted -= HandleReloadStarted;
		shooter.SignalReloadStopped -= HandleReloadStopped;
		shooter.SignalReloadFinished -= HandleReloadFinished;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.S)) {
			shooter.TurnOnShield();
		}
		else if (Input.GetKeyUp(KeyCode.S)) {
			shooter.TurnOffShield();
		}

		if (shooter.isReloading) {
			entityUIConnection.reloadProgressBar.progressBar.Value = shooter.reloadProgress;
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
					else if (shooter.CanReload()) shooter.StartReloading();
				}
			}
			else if (touchType == TouchType.OnRelease) {
				if (shooter.isAutoFiring) shooter.StopAutoFiring();
				if (shooter.isReloading) shooter.StopReloading();
			}
		}
	}

	void HandleReloadStarted() {
		entityUIConnection.reloadProgressBar.progressBar.Value = 0;
		entityUIConnection.reloadProgressBar.Show();
	}

	void HandleReloadFinished() {
		entityUIConnection.reloadProgressBar.Hide();
	}

	void HandleReloadStopped() {
		entityUIConnection.reloadProgressBar.Hide();
	}

	void HandleRightTouch(TouchPhase phase) {

	}

	void HandleLeftTouch(TouchPhase phase) {

	}
}
