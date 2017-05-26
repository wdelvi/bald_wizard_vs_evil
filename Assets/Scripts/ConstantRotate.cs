using UnityEngine;
using System.Collections;

public class ConstantRotate : MonoBehaviour 
{
    public float speed;
	private Rigidbody2D rb2D;

    void Start() 
	{
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
	{
        rb2D.MoveRotation(rb2D.rotation + speed * Time.fixedDeltaTime);
    }
}