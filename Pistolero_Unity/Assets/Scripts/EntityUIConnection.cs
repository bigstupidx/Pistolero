using UnityEngine;
using System.Collections;

public class EntityUIConnection : MonoBehaviour {
	public tk2dTextMesh ammoLabel;
	public tk2dTextMesh healthLabel;
	public ReloadProgressBar reloadProgressBar;

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

		if (gun.bulletsLeft == 0) t = "RELOAD!";
		else {
			for (int i = 0; i < gun.bulletsLeft; i++) {
				t += "l ";
			}
		}
		
		if (ammoLabel) ammoLabel.text = t;

		healthLabel.text = health.hp.ToString();
	}
}
