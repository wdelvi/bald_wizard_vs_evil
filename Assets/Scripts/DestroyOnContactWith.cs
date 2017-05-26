using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContactWith : MonoBehaviour 
{
	public GameObject toDestoryIfCollide;
	public float secondsAfterCollisionToDestroy;

	private bool triggered;

	// Use this for initialization
	void Start () 
	{
		triggered = false;
	}
	
	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (!triggered && coll.gameObject == toDestoryIfCollide) 
		{
			triggered = true;
			Destroy (this.gameObject, secondsAfterCollisionToDestroy);
		}
	}
}
