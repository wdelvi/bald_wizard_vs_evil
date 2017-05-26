using UnityEngine;
using System.Collections;

public class DestroyPastX: MonoBehaviour 
{
	public float destroyIfXLessThan;
	private Transform myTransform;

	void Start()
	{
		this.myTransform = this.GetComponent<Transform> ();
	}

	void Update () 
	{
		if (this.myTransform == null) 
		{
			return;
		}

		if (this.myTransform.localPosition.x < destroyIfXLessThan) 
		{
			Destroy (this.gameObject, 0f);
		}
	}
}
