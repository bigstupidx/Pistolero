using UnityEngine;
using System.Collections;

public class EntitySpawnPointHelper : MonoBehaviour {
	public string spawnPointName;

	// Use this for initialization
	void Start () {
		Transform spawnPoint = GameObject.Find(spawnPointName).transform;
		transform.position = spawnPoint.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
