using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public Transform transformRaised;
	public Transform transformLowered;
	public float damageMultiplier = 0.3f;
	public bool isRaised {get; private set;}

	private DamageableBody damageableBody;

	// Use this for initialization
	void Start () {
		isRaised = false;
		damageableBody = GetComponentInParent<DamageableBody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Raise() {
		if (isRaised) Debug.LogWarning("raising shield when already raised!");

		isRaised = true;

		Go.killAllTweensWithTarget(transform);

		transform.parent = transformRaised;

		Go.to(transform, 0.1f, new GoTweenConfig().localPosition(Vector3.zero).localRotation(Quaternion.identity));
	}

	public void Lower() {
		if (!isRaised) Debug.LogWarning("lowering shield when already lowered!");

		isRaised = false;

		Go.killAllTweensWithTarget(transform);
		
		transform.parent = transformLowered;
		
		Go.to(transform, 0.1f, new GoTweenConfig().localPosition(Vector3.zero).localRotation(Quaternion.identity));
	}

	void OnTriggerEnter(Collider coll) {
		Bullet bullet = coll.GetComponent<Bullet>();

		if (bullet) {
			// don't collide with the person who shot the bullet
			if (bullet.rootTransformOfOrigin == transform.root) return;

			// if it's not raised, the bullet shouldn't hit the shield at all
			if (isRaised) damageableBody.Damage(bullet.damage * damageMultiplier);
		}
	}
}
