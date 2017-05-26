using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public GameSettings settings;
	public GameObject currentProjectile;

	private float stunTimer;

	// Use this for initialization
	protected void Start () 
	{
		this.stunTimer = this.settings.stunLength;
	}
	
	// Update is called once per frame
	public void UpdatePlayer () 
	{
		if (this.stunTimer >= this.settings.stunLength) 
		{
			this.UpdatePlayerPosition ();
			this.CheckForInput ();
		}
	}

	protected void UpdatePlayerPosition()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		this.transform.localPosition = new Vector3 (this.transform.localPosition.x, mousePos.y, this.transform.localPosition.z);
	}

	protected void CheckForInput()
	{
		if (this.currentProjectile == null) 
		{
			return;
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 spawnLocation = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, this.currentProjectile.transform.localPosition.z); 
			Instantiate (this.currentProjectile, spawnLocation, Quaternion.identity);
		}
	}
}
