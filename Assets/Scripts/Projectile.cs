using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float damage;
	public float speed;
	private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () 
	{
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb2D.AddForce (new Vector2 (speed, 0f));
	}
}
