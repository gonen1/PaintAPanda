using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicEnemy : MonoBehaviour {

	public GameObject sprite;

	[System.Serializable]
	public class EnemyStats
	{
		public int lane;
		public float maxSpeed;
		public int hp = 1;
		public float speedMod;
		public float speed;
	}

	public EnemyStats enemyStats = new EnemyStats();
	// Use this for initialization
	void Start () {
		enemyStats.speed = Random.Range (1, enemyStats.maxSpeed + 1) * enemyStats.speedMod;
		enemyStats.lane = Random.Range (1, 4);
		if (enemyStats.lane == 1) {
			transform.position = new Vector3 (10.5f, 2f, 0);
		} else if (enemyStats.lane == 2) {
			transform.position = new Vector3 (10.5f, 0, 0);
		} else if (enemyStats.lane == 3) {
			transform.position = new Vector3 (10.5f, -2.75f, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.left * enemyStats.speed * Time.deltaTime;
		if (transform.position.x <= -15) {
			DestroyObject (sprite);
		}
	}

	void OnCollisionEnter2D(Collision2D _colInfo)
	{
		enemyStats.hp -= 1;
		if (enemyStats.hp == 0) {
			DestroyObject (sprite);
		}
	}
}
