using UnityEngine;
using System.Collections;

public class ControllerEnemy : MonoBehaviour {
	public Shooter shooter;

	void Start () {
		StartCoroutine(AttackLoop());
	}
	
	void Update () {

	}

	IEnumerator AttackLoop() {
		while (shooter.gun.HasBulletsLeft()) {
			shooter.Fire();
			yield return new WaitForSeconds(Random.Range(0, 0.5f));
		}

		shooter.shield.Raise(0.1f);

		yield return new WaitForSeconds(Random.Range(0.5f, 2f));

		shooter.shield.Lower(0.1f);
		StartCoroutine(shooter.Reload());

		while (shooter.isReloading) yield return null;

		shooter.shield.Raise(0.1f);
		
		yield return new WaitForSeconds(Random.Range(0.5f, 2f));

		shooter.shield.Lower(0.1f);

		StartCoroutine(AttackLoop());
	}
}
