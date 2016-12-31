using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave 
	{
		public string name;
		public BasicEnemy[] enemies;
		public int count;
		public float rate;
		public float maxSpeed;
	}

	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	private int enemiesSpawned = 0;

	private SpawnState state = SpawnState.COUNTING;

	void Start()
	{
		waveCountdown = timeBetweenWaves;
	}
		
	void Update()
	{
		if (state == SpawnState.WAITING) {
			if (!EnemyIsAlive ()) {
				//Begin a new round
				WaveCompleted();
			} else {
				return;
			}
		}

		if (waveCountdown <= 0) {
			if (state != SpawnState.SPAWNING) {
				StartCoroutine (SpawnWave (waves [nextWave]));
			}
		} else {
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave completed");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 0;
			Debug.Log ("ALL WAVES COMPLETE! Looping..");
		} else {
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		enemiesSpawned = 0;
		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++) {
			BasicEnemy enemy = _wave.enemies [Random.Range (0, _wave.enemies.Length)];
			SpawnEnemy (enemy);
			enemiesSpawned += 1;
			yield return new WaitForSeconds (1f / (_wave.rate * enemiesSpawned));
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy (BasicEnemy _enemy)
	{
		BasicEnemy enemy = Instantiate (_enemy);
		enemy.enemyStats.maxSpeed = 2;
		enemy.enemyStats.speedMod = 1;
		enemy.enemyStats.hp = 1;
		Debug.Log("Spawning Enemy: " + _enemy.name);
	}
}
