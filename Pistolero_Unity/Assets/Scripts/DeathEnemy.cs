using UnityEngine;
using System.Collections;

public class DeathEnemy : Death {
	void Start() {
		Init();
	}

	override public void Init() {
		base.Init();
	}

	override public void Die() {
		base.Die();
		Debug.Log("enemy dead");
	}
}
