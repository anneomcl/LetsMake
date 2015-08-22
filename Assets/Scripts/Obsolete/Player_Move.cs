using UnityEngine;
using System.Collections;

public class Player_Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float moveSpeed = 10f;
	public float turnSpeed = 100f;
	public float jumpSpeed = 10f;

	//private Vector3 moveDirection = Vector3.zero;

	// Update is called once per frame
	void Update () {

		//Vector3 pos = transform.position;

		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate (-Vector3.forward * moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate (Vector3.up * -turnSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate (Vector3.up * turnSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.Space))
		{
			transform.Translate (Vector3.up * jumpSpeed * Time.deltaTime);
		}
		//transform.position = pos;
	}
}
