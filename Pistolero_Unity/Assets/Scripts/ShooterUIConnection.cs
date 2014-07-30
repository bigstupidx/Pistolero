using UnityEngine;
using System.Collections;

public class ShooterUIConnection : MonoBehaviour {
	public tk2dTextMesh ammoLabel;
	public tk2dTextMesh healthLabel;

	private Gun gun;
	private Health health;

	// Use this for initialization
	void Start () {
		gun = GetComponentInChildren<Gun>();
		health = GetComponentInChildren<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		string t = "";
		
		for (int i = 0; i < gun.bulletsLeft; i++) {
			t += "l ";
		}
		
		ammoLabel.text = t;

		healthLabel.text = health.hp.ToString();
	}
}
