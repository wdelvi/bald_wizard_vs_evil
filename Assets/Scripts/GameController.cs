using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	protected GameSettings settings;

	// Use this for initialization
	void Start () 
	{
		this.settings = this.gameObject.GetComponent<GameSettings> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.settings.player != null) 
		{
			this.settings.player.UpdatePlayer ();
		}
	}
}
