using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	public bool isDead {get; private set;}

	void Start() {
		Init();
	}

	virtual public void Init() {
		isDead = false;
	}

	virtual public void Die() {
		if (isDead) Debug.LogWarning("already dead; can't die again!");

		isDead = true;
		Destroy(this.gameObject);
	}
}
