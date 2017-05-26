using UnityEngine;
using System.Collections;

public class GameModel 
{
	public bool gameRunning;
	public float timeUntilNextSpawn;
	public float spawnRateDecrease;
	public float spawnTimer;

	public void GameStart( GameSettings settings )
	{
		this.timeUntilNextSpawn = settings.spawnRateStart;
		this.spawnRateDecrease = (settings.spawnRateStart - settings.spawnRateFloor) / settings.gameLength;
		this.gameRunning = true;
	}

	public void AdjustSpawnTimer( GameSettings settings )
	{
		this.spawnTimer = 0f;

		this.timeUntilNextSpawn -= this.spawnRateDecrease;

		if (this.timeUntilNextSpawn < settings.spawnRateFloor) 
		{
			this.timeUntilNextSpawn = settings.spawnRateFloor;
		}
	}
}
