using UnityEngine;
using System.Collections;

public class EnemyInitializer : MonoBehaviour {
	public Shield shieldPrefab;
	public Gun gunPrefab;

	public void Init () {
		Shooter shooter = GetComponentInChildren<Shooter>();

		Gun gun = (Gun)Instantiate(gunPrefab);
		shooter.AddGun(gun);

		Shield shield = (Shield)Instantiate(shieldPrefab);
		shooter.AddShield(shield);
	}
}
