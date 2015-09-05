using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour 
{
	public float moveSpeed;
	public float jumpHeight;
	public float gravity;

	private Rigidbody myRigidbody;


	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody> ();//rigidbody2D = GetComponent<Rigidbody2D> ();
		Physics.gravity = new Vector3 (0, gravity);
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpHeight);
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y);//rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
		}
		else if (Input.GetKey (KeyCode.D)) 
		{
			myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y);//rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
		}

		if (transform.position.x >= 20.2)
			print ("Camera could spin");
		//if(transform.position

	}
}
