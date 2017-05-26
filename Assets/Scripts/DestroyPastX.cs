using UnityEngine;
using System.Collections;

public class DestroyPastX: MonoBehaviour 
{
	public float xForDestroy;
	public bool isLessThan;
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

		if (this.isLessThan) 
		{
			if (this.myTransform.localPosition.x < xForDestroy) 
			{
				Destroy (this.gameObject, 0f);
			}
		}
		else 
		{
			if (this.myTransform.localPosition.x > xForDestroy) 
			{
				Destroy (this.gameObject, 0f);
			}
		}
	}
}
