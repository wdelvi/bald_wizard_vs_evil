using UnityEngine;
using System.Collections;

public class DestroyOnTimer : MonoBehaviour 
{
	public float secondsUntilDestroy;
	// Use this for initialization
	void Start () 
	{
		Destroy (this.gameObject, secondsUntilDestroy);
	}
}
