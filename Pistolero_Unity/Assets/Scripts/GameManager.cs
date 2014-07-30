using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Transform enemySpawn;
	public GameObject basicEnemyPrefab;

	// Use this for initialization
	void Start () {
		GameObject newEnemy = SpawnBasicEnemy();
		newEnemy.GetComponent<ControllerEnemy>().StartAttackLoop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject SpawnBasicEnemy() {
		GameObject enemy = (GameObject)Instantiate(basicEnemyPrefab);
		enemy.GetComponent<EnemyInitializer>().Init();
		enemy.transform.parent = enemySpawn;
		enemy.transform.localPosition = Vector3.zero;
		return enemy;
	}
}
