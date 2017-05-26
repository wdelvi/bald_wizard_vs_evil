using UnityEngine;
using System.Collections;

public class GameModel 
{
	public bool gameRunning;
	public float timeUntilNextSpawn;
	public float spawnRateDecrease;
	public float spawnTimer;
	public float numberOfSpawns;
	public float startTime;
	public float gameProgress;

	public void GameStart( GameSettings settings )
	{
		this.timeUntilNextSpawn = settings.spawnRateStart + settings.firstSpawnAddittionalDelay;
		this.numberOfSpawns = 0f;
		this.spawnRateDecrease = (settings.spawnRateStart - settings.spawnRateFloor) / settings.gameLength;
		this.gameRunning = true;
		this.startTime = Time.time;
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
