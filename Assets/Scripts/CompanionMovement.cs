using UnityEngine;
using System.Collections;


public class CompanionMovement : MonoBehaviour {

	private Transform playerTransform;
	public float regularSpeed, catchUpSpeed;
	public float distanceLimitMin, distanceLimitMax;

	void Start ()
	{
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null)
		{
			playerTransform= playerObject.GetComponent <Transform>();
		}

	}

	void Update () 
	{	
		transform.LookAt (playerTransform.position);
		if (Vector3.Distance(playerTransform.position, transform.position)> distanceLimitMin && Vector3.Distance(playerTransform.position, transform.position)<= distanceLimitMax)
			transform.Translate(Vector3.forward * Time.deltaTime*regularSpeed);
		if (Vector3.Distance(playerTransform.position, transform.position)> distanceLimitMax)
			transform.Translate(Vector3.forward * Time.deltaTime*catchUpSpeed);

	}
}
