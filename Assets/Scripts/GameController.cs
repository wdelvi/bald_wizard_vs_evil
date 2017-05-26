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
		this.UpdateMainText ("Oh no! Bugs are attacking Lumos! We need the Bald Wizard!\n(Click to Debug)", 5f);
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

		if (this.settings.building == null) 
		{
			this.LoseGame ();
		}

		this.UpdateProgressSlider ();

		if (this.model.gameProgress >= 1f) 
		{
			this.WinGame ();
		}
	}

	protected void LoseGame()
	{
		this.UpdateMainText ( "Lumos has been overrun with bugs!", 0f );
		this.model.gameRunning = false;
	}

	protected void WinGame()
	{
		this.UpdateMainText ( "Lumos is saved!\nWe will miss our Bald Wizard...\nSee you soon!", 0f );
		this.model.gameRunning = false;
	}

	protected void UpdateProgressSlider()
	{
		float timeInGame = Time.time - this.model.startTime;
		this.model.gameProgress = timeInGame / this.settings.gameLength;

		if (this.settings.progressSlider != null) 
		{
			this.settings.progressSlider.value = this.model.gameProgress;
		}
	}

	protected void UpdateMainText( string newText, float clearTime )
	{
		if ( this.settings != null && this.settings.mainText != null) 
		{
			this.settings.mainText.text = newText;

			if (clearTime > 0f) 
			{
				Invoke ("ClearMainText", clearTime);
			}
		}
	}

	protected void ClearMainText()
	{
		this.UpdateMainText ("", 0f);
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

		GameObject newBug = Instantiate (this.settings.bug, this.GetSpawnBugLocation (), this.settings.bug.transform.rotation) as GameObject;
		DestroyOnContactWith newBugDestroyOnContact = newBug.GetComponent<DestroyOnContactWith> ();

		if (this.settings.building != null && newBugDestroyOnContact != null) 
		{
			newBugDestroyOnContact.toDestoryIfCollide = this.settings.building;
		}
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
