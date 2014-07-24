using UnityEngine;
using System.Collections;

public class ShooterPlayerController : MonoBehaviour {
	public Gun gun;
	public Shield shield;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (gun.CanFire()) gun.Fire();
		}
		else if (Input.GetMouseButton(0)) {
			if (gun.isAutomatic && gun.CanFire()) gun.Fire();
		}
		else if (Input.GetMouseButtonUp(0)) {

		}

		if (Input.GetKeyDown(KeyCode.S)) {
			if (shield.isRaised) shield.Lower();
			else shield.Raise();
		}
	}
}
