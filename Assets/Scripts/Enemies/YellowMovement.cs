//DO NOT CONFUSE with Yellow Teleporter, it is completely unrelated

using UnityEngine;
using System.Collections;

public class YellowMovement : MonoBehaviour
{

	public float moveSpeed = 10f;
	public float turnSpeed = 100f;
	public float jumpSpeed = 10f;
	public float speed = 2.0f;

	private bool dirRight = true;

	// Update is called once per frame
	IEnumerator delay()
	{
		yield return new WaitForSeconds (1f);
		print ("done with waiting");
	}

	void Update ()
	{
		transform.Rotate (Vector3.up * turnSpeed * Time.deltaTime);
		if (dirRight) {
			//transform.Rotate (Vector3.up * turnSpeed * Time.deltaTime);
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		} else {
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}
		if(transform.position.x >= 0.0f) {
			dirRight = false;
		}
		
		if(transform.position.x <= -1) {
			dirRight = true;
		}
	}
}
