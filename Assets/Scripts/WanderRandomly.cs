using UnityEngine;
using System.Collections;

public class WanderRandomly : MonoBehaviour {


	public float xMax, xMin, zMax, zMin;
	public float maxSpeed;
	public float turnRadius;
	public float smoothTime;
	private Vector3[] directions; 
	private int index;
	private Vector3 targetDirection;
	private Vector3 velocity = Vector3.zero;



	void Start () 
	{	
		directions = new Vector3[4] {Vector3.forward, Vector3.back, Vector3.left, Vector3.right}; 
	
	}
	

	void FixedUpdate () 
	{

		index = Random.Range (0, directions.Length);
		targetDirection = directions [index];
		transform.position = Vector3.SmoothDamp (transform.position, targetDirection * turnRadius, ref velocity, smoothTime, maxSpeed);
	
	}
}
