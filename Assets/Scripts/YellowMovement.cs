using UnityEngine;
using System.Collections;

public class YellowMovement : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	public float moveSpeed = 10f;
	public float turnSpeed = 100f;
	public float jumpSpeed = 10f;
	// Update is called once per frame
	IEnumerator delay(){
	
		yield return new WaitForSeconds (1f);
		print ("done with waiting");
	}

	private bool dirRight = true;
	public float speed = 2.0f;

	void Update ()
	{
		transform.Rotate (Vector3.up * turnSpeed * Time.deltaTime);
		if (dirRight)
			//transform.Rotate (Vector3.up * turnSpeed * Time.deltaTime);
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		else
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		
		if(transform.position.x >= 0.0f) {
			dirRight = false;
		}
		
		if(transform.position.x <= -1) {
			dirRight = true;
		}

	//		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
//		StartCoroutine (delay ());
 //			transform.Translate (-Vector3.forward * moveSpeed * Time.deltaTime);
//		StartCoroutine (delay ());
//			transform.Rotate (Vector3.up * -turnSpeed * Time.deltaTime);

//			transform.Rotate (Vector3.up * turnSpeed * Time.deltaTime);
			//transform.Translate (Vector3.up * jumpSpeed * Time.deltaTime);

			//transform.Translate (Vector3.down * jumpSpeed * Time.deltaTime);


	}
}

