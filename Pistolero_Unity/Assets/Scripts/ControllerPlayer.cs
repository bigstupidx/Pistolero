using UnityEngine;
using System.Collections;

public class ControllerPlayer : MonoBehaviour {
	public Shooter shooter;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (shooter.shield && shooter.gun) {
			if (Input.GetMouseButtonDown(0)) {
				if (shooter.CanFire()) shooter.Fire();
				else if (shooter.CanReload()) StartCoroutine(shooter.Reload());
			}
			else if (Input.GetMouseButton(0)) {
				if (shooter.gun.isAutomatic && shooter.CanFire()) shooter.Fire();
			}
			else if (Input.GetMouseButtonUp(0)) {

			}

			if (Input.GetKeyDown(KeyCode.S)) {
				if (shooter.shield.isRaised) shooter.shield.Lower(0.1f);
				else shooter.shield.Raise(0.1f);
			}
		}
	}
}
