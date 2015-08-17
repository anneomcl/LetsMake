using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("HIT");
			Application.LoadLevel("GameOver");
		}
	}
}
