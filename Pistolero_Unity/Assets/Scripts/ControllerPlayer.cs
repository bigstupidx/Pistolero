using UnityEngine;
using System.Collections;

public class ControllerPlayer : MonoBehaviour {
	private Shooter shooter;
	
	// Use this for initialization
	void Start () {
		shooter = GetComponentInChildren<Shooter>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (shooter.CanFire()) {
				if (shooter.gun.isAutomatic) shooter.StartAutoFiring();
				else shooter.Fire(true);
			}
			else if (shooter.CanReload()) shooter.Reload();
		}
		else if (Input.GetMouseButtonUp(0)) {
			if (shooter.isAutoFiring) shooter.StopAutoFiring();
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			shooter.RaiseShield();
		}
		else if (Input.GetKeyUp(KeyCode.S)) {
			shooter.LowerShield();
		}
	}
}
