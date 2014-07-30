using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DebugManager))]

public class DebugManagerEditor : Editor {
	private Gun[] gunPrefabs;
	private Shield[] shieldPrefabs;
	
	void OnEnable () {
		var rawGuns = Resources.LoadAll("Prefabs/Guns");
		var rawShields = Resources.LoadAll("Prefabs/Shields");
		
		gunPrefabs = new Gun[rawGuns.Length];
		shieldPrefabs = new Shield[rawShields.Length];
		
		for (int i = 0; i < rawGuns.Length; i++) {
			Gun gun = ((GameObject)rawGuns[i]).GetComponent<Gun>();
			if (gun) gunPrefabs[i] = gun;
			else Debug.LogWarning("non-gun in gun folder!");
		}
		
		for (int i = 0; i < rawShields.Length; i++) {
			Shield shield = ((GameObject)rawShields[i]).GetComponent<Shield>();
			if (shield) shieldPrefabs[i] = shield;
			else Debug.LogWarning("non-shield in shield folder!");
		}
	}
	
	override public void OnInspectorGUI() {
		//base.OnInspectorGUI();

		var debugManager = target as DebugManager;

//		Shooter[] shooters = new Shooter[] {
//			debugManager.pistolero.GetComponentInChildren<Shooter>(),
//			debugManager.badGuy.GetComponentInChildren<Shooter>()
//		};

		//for (int i = 0; i < 2; i++) {
			//Shooter s = shooters[i];
			Shooter s = debugManager.pistolero.GetComponentInChildren<Shooter>();

//			if (i == 0) EditorGUILayout.LabelField("Pistolero");
//			else {
//				EditorGUILayout.Separator();
//				EditorGUILayout.LabelField("Bad Guy");
//			}

			if (s.gun == null) {
				foreach (Gun gun in gunPrefabs) {
					if (GUILayout.Button("Add " + gun.name)) {
						s.AddGun((Gun)Instantiate(gun));
						break;
					}
				}
			}
			else {
				if (GUILayout.Button("Remove " + s.gun.name)) {
					s.RemoveGun();
				}
			}
			
			EditorGUILayout.Separator();
			
			if (s.shield == null) {
				foreach (Shield shield in shieldPrefabs) {
					if (GUILayout.Button("Add " + shield.name)) {
						s.AddShield((Shield)Instantiate(shield));
						break;
					}
				}
			}
			else {
				if (GUILayout.Button("Remove " + s.shield.name)) {
					s.RemoveShield();
				}
			}

			EditorUtility.SetDirty(s);
		//}
	}
}