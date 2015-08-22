using UnityEngine;
using System.Collections;

public class RedTeleport : MonoBehaviour {


	
	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.CompareTag("Player"))
		{
			print ("TeleHit");
			Application.LoadLevel("RedLevel");
		}
	}
}