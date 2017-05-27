using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel 
{
	public bool gameRunning;
	public float timeUntilNextSpawn;
	public float spawnRateDecrease;
	public float spawnTimer;
	public float numberOfSpawns;
	public float startTime;
	public float gameProgress;
	public bool gameWon;
	public bool gameLost;
	public int goodbyeIndex;
	public bool bossSpawned;
	public bool gameStarted;
	public bool gameEnded;
	public List<GameObject> spawnedEnemies;

	public void GameStart( GameSettings settings )
	{
		this.timeUntilNextSpawn = settings.spawnRateStart + settings.firstSpawnAddittionalDelay;
		this.numberOfSpawns = 0f;
		this.spawnRateDecrease = (settings.spawnRateStart - settings.spawnRateFloor) / settings.gameLength;
		this.gameRunning = true;
		this.startTime = Time.time;
		this.gameWon = false;
		this.gameLost = false;
		this.goodbyeIndex = 0;
		this.bossSpawned = false;
		this.gameStarted = false;
		this.gameEnded = false;
		this.spawnedEnemies = new List<GameObject> ();
	}

	public void AdjustSpawnTimer( GameSettings settings )
	{
		this.spawnTimer = 0f;
		this.numberOfSpawns += 1f;

		this.timeUntilNextSpawn = settings.spawnRateStart - (this.spawnRateDecrease * numberOfSpawns);

		if (this.timeUntilNextSpawn < settings.spawnRateFloor) 
		{
			this.timeUntilNextSpawn = settings.spawnRateFloor;
		}
	}
}
