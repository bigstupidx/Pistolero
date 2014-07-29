using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DebugManager : MonoBehaviour {
	public GameObject pistolero;
	public GameObject badGuy;

	void OnEnable () {
		pistolero = GameObject.Find("Pistolero");
		badGuy = GameObject.Find("Bad Guy");
	}
}
