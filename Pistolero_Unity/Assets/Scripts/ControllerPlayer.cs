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
		if (shooter.shield && shooter.gun) {
			if (Input.GetMouseButtonDown(0)) {
				if (shooter.CanFire()) shooter.Fire();
				else if (shooter.CanReload()) shooter.Reload();
			}
			else if (Input.GetMouseButton(0)) {
				if (shooter.gun.isAutomatic && shooter.CanFire()) shooter.Fire();
			}
			else if (Input.GetMouseButtonUp(0)) {

			}

			if (Input.GetKeyDown(KeyCode.S)) {
				if (shooter.shield.isRaised) shooter.LowerShield();
				else shooter.RaiseShield();
			}
		}
	}
}
