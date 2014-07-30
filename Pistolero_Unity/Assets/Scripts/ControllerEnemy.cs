using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerEnemy : MonoBehaviour {
	public float minShieldTime = 0.5f;
	public float maxShieldTime = 1.5f;
	public float minBaseActionTime = 0.1f;
	public float maxBaseActionTime = 0.5f;

	public float randomWeightUseGun = 1.5f;
	public float randomWeightUseShield = 1.0f;

	public float cancelReloadChance = 0.05f;
	
	public enum AttackAction {
		Fire,
		Reload,
		RaiseShield,
		LowerShield,
		NONE
	}

	private Entity entity;

	void Awake() {
		entity = GetComponent<Entity>();
	}

	void Start() {
		ShooterUIConnection uiConn = GetComponent<ShooterUIConnection>();
		uiConn.ammoLabel = GameObject.Find("Enemy Ammo Label").GetComponent<tk2dTextMesh>();
		uiConn.healthLabel = GameObject.Find("Enemy Health Label").GetComponent<tk2dTextMesh>();
	}
	
	void Update () {

	}

	public void StartAttackLoop() {
		StartCoroutine(AttackLoop());
	}

	IEnumerator AttackLoop() {
		AttackAction a = GetNextAttackAction();

		if (a == AttackAction.Fire) {
			entity.shooter.Fire();
		}
		else if (a == AttackAction.Reload) {
			entity.shooter.Reload();
			while (entity.shooter.isReloading) {
				if (Random.value < cancelReloadChance) break;
				else yield return null;
			}
		}
		else if (a == AttackAction.LowerShield) {
			entity.shooter.LowerShield();
		}
		else if (a == AttackAction.RaiseShield) {
			entity.shooter.RaiseShield();
			yield return new WaitForSeconds(Random.Range(minShieldTime, maxShieldTime));
		}

		yield return new WaitForSeconds(Random.Range(minBaseActionTime, maxBaseActionTime));

		StartCoroutine(AttackLoop());
	}

	AttackAction GetNextAttackAction() {
		List<AttackAction> possibleActions = new List<AttackAction>();

		// if the shield is raised, the only option is to lower it
		if (entity.shooter.shield.isRaised) return AttackAction.LowerShield;

		else if (entity.shooter.CanReload()) {
			possibleActions.Add(AttackAction.Reload);
			possibleActions.Add(AttackAction.RaiseShield);
		}

		else if (entity.shooter.CanFire()) {
			possibleActions.Add(AttackAction.Fire);
			possibleActions.Add(AttackAction.RaiseShield);
		}

		float totalWeight = 0;

		foreach (AttackAction a in possibleActions) {
			if (a == AttackAction.Fire || a == AttackAction.Reload) totalWeight += randomWeightUseGun;
			if (a == AttackAction.RaiseShield) totalWeight += randomWeightUseShield;
		}

		float rand = Random.Range(0, totalWeight);
		float countUp = 0;

		foreach (AttackAction a in possibleActions) {
			if (a == AttackAction.Fire || a == AttackAction.Reload) countUp += randomWeightUseGun;
			if (a == AttackAction.RaiseShield) countUp += randomWeightUseShield;

			if (countUp >= rand) return a;
		}

		Debug.LogWarning("didn't actually pick a random choice correctly");

		return AttackAction.NONE;
	}
}
