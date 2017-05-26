using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
		this.UpdateMainText ("Oh no! Bugs are attacking Lumos! We need the Bald Wizard!\nClick to Debug", 8f);
	}

	// Update is called once per frame
	void Update () 
	{
		if ( this.settings == null || this.model == null ) 
		{
			return;
		}

		if (!this.model.gameRunning && Input.GetMouseButtonDown (0) ) 
		{
			if (this.model.gameWon) 
			{
				this.ShowNextGoodbye ();
			} 
			else if (this.model.gameLost)
			{
				this.RestartGame ();
			}
		}

		if (!this.model.gameRunning) 
		{
			return;
		}

		if (this.settings.player != null) 
		{
			this.settings.player.UpdatePlayer ();
		}

		this.CheckToSpawnBug ();

		this.CheckToSpawnElevate ();

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
		this.EndGame ();

		this.UpdateMainText ( "Lumos has been overrun with bugs!\nClick to restart", 0f );
		Invoke ("DelayedSetGameLost", 2f);
	}

	protected void DelayedSetGameLost()
	{
		this.model.gameLost = true;
	}

	protected void WinGame()
	{
		this.EndGame ();

		this.UpdateMainText ( "Lumos is saved!\nGoodbye Bald Wizard!\nClick for special thanks", 0f );
		Invoke ("DelayedSetGameWon", 2f);
	}

	protected void DelayedSetGameWon()
	{
		this.model.gameWon = true;
	}

	protected void EndGame()
	{
		this.model.gameRunning = false;

		this.StopAllEnemies ();
	}

	protected void StopAllEnemies()
	{
		foreach (GameObject enemy in this.model.spawnedEnemies) 
		{
			if (enemy != null) 
			{
				Projectile enemyProjectile = enemy.GetComponent<Projectile> ();

				if (enemyProjectile != null) 
				{
					enemyProjectile.speed = 0f;
				}

				Rigidbody2D enemyRigidBody = enemy.GetComponent<Rigidbody2D> ();

				if (enemyRigidBody != null) 
				{
					enemyRigidBody.bodyType = RigidbodyType2D.Static;
				}
			}
		}
	}

	protected void RestartGame()
	{
		SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
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

	protected void CheckToSpawnElevate()
	{
		if( this.model.gameProgress >= this.settings.bossSpawnTimeRatio && !this.model.bossSpawned )
		{
			this.SpawnElevate ();
		}
	}

	protected void SpawnBug()
	{
		if (this.settings.bugPrefab == null) 
		{
			return;
		}

		GameObject newBug = Instantiate (this.settings.bugPrefab, this.GetSpawnBugLocation (), this.settings.bugPrefab.transform.rotation) as GameObject;
		DestroyOnContactWith newBugDestroyOnContact = newBug.GetComponent<DestroyOnContactWith> ();

		if (this.settings.building != null && newBugDestroyOnContact != null) 
		{
			newBugDestroyOnContact.toDestoryIfCollide = this.settings.building;
		}

		this.model.spawnedEnemies.Add (newBug);
	}

	protected void SpawnElevate()
	{
		if (this.settings.elevatePrefab == null) 
		{
			return;
		}

		Vector3 spawnLocation = new Vector3( this.settings.bossSpawnX, 0f, this.settings.elevatePrefab.transform.localPosition.z );
		GameObject newElevate = Instantiate (this.settings.elevatePrefab, spawnLocation, this.settings.elevatePrefab.transform.rotation) as GameObject;
		DestroyOnContactWith newElevateDestroyOnContact = newElevate.GetComponent<DestroyOnContactWith> ();
		Rigidbody2D newElevateRigidBody = newElevate.GetComponent<Rigidbody2D> ();
		Projectile newElevateProjectile = newElevate.GetComponent<Projectile> (); 

		if (this.settings.building != null && newElevateDestroyOnContact != null) 
		{
			newElevateDestroyOnContact.toDestoryIfCollide = this.settings.building;
		}

		if (newElevateRigidBody != null && newElevateProjectile != null) 
		{
			Debug.Log ("Applied force!");
			newElevateRigidBody.AddForce (new Vector2 (newElevateProjectile.speed * this.settings.bossInitialForceMultiplier, 0f));
		}

		this.model.bossSpawned = true;
		this.model.spawnedEnemies.Add (newElevate);
	}

	protected Vector3 GetSpawnBugLocation( )
	{
		Vector3 spawnLocation = new Vector3 ( 0f, 0f, 0f );
		spawnLocation.x = this.settings.spawnX;
		spawnLocation.y = Random.Range (this.settings.minSpawnY, this.settings.maxSpawnY);
		spawnLocation.z = this.settings.bugPrefab.transform.localPosition.z;
		return spawnLocation;
	}

	protected void ShowNextGoodbye()
	{
		if (this.settings.goodbyes.Count <= 0) 
		{
			return;
		}
			
		if (this.model.goodbyeIndex >= this.settings.goodbyes.Count) 
		{
			this.model.goodbyeIndex = 0;
		}

		this.UpdateMainText (this.settings.goodbyes [this.model.goodbyeIndex], 0f);

		this.model.goodbyeIndex++;
	}
}
