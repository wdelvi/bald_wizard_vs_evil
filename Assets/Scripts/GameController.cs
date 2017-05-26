using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	protected GameSettings settings;
	protected GameModel model;

	// Use this for initialization
	void Start () 
	{
		this.settings = this.gameObject.GetComponent<GameSettings> ();
		this.model = new GameModel ();
		this.model.GameStart (settings);
	}

	// Update is called once per frame
	void Update () 
	{
		if ( this.settings == null || this.model == null || !this.model.gameRunning ) 
		{
			return;
		}

		if (this.settings.player != null) 
		{
			this.settings.player.UpdatePlayer ();
		}

		this.CheckToSpawnBug ();
	}

	protected void CheckToSpawnBug()
	{
		this.model.spawnTimer += Time.deltaTime;

		if (this.model.spawnTimer >= this.model.timeUntilNextSpawn) 
		{
			this.SpawnBug ();
			this.model.AdjustSpawnTimer (settings);
		}
	}

	protected void SpawnBug()
	{
		if (this.settings.bug == null) 
		{
			return;
		}

		Instantiate (this.settings.bug, this.GetSpawnBugLocation (), Quaternion.identity);
	}

	protected Vector3 GetSpawnBugLocation()
	{
		Vector3 spawnLocation = new Vector3 ( 0f, 0f, 0f );
		spawnLocation.x = this.settings.spawnX;
		spawnLocation.y = Random.Range (this.settings.minSpawnY, this.settings.maxSpawnY);
		spawnLocation.z = this.settings.bug.transform.localPosition.z;
		return spawnLocation;
	}
}
