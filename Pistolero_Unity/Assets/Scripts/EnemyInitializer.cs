using UnityEngine;
using System.Collections;

public class EnemyInitializer : MonoBehaviour {
	public Shield shieldPrefab;
	public Gun gunPrefab;
	public Shooter shooterPrefab;

	public void Init () {
		Shooter shooter = (Shooter)Instantiate(shooterPrefab);
		shooter.transform.parent = transform;
		shooter.transform.localPosition = Vector3.zero;
		shooter.transform.localRotation = Quaternion.identity;

		Gun gun = (Gun)Instantiate(gunPrefab);
		shooter.AddGun(gun);

		Shield shield = (Shield)Instantiate(shieldPrefab);
		shooter.AddShield(shield);
	}
}
