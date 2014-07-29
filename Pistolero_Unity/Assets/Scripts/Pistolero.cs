using UnityEngine;
using System.Collections;

public class Pistolero : MonoBehaviour {
	public Shooter shooter;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (shooter.shield && shooter.gun) {
			if (Input.GetMouseButtonDown(0)) {
				if (!shooter.shield.isRaised && shooter.gun.EnoughTimeHasLapsedForNextFire()) shooter.gun.Fire();
			}
			else if (Input.GetMouseButton(0)) {
				if (shooter.gun.isAutomatic && shooter.gun.EnoughTimeHasLapsedForNextFire()) shooter.gun.Fire();
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
