using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	public Shooter shooter {
		get {
			return GetComponentInChildren<Shooter>();
		}
	}

	public Gun gun {
		get {
			return GetComponentInChildren<Gun>();
		}
	}

	public Shield shield {
		get {
			return GetComponentInChildren<Shield>();
		}
	}

	public Health health {
		get {
			return GetComponent<Health>();
		}
	}
}
