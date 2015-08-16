using UnityEngine;
using System.Collections;

public class BlueTeleport : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{


		if(other.gameObject.CompareTag("Player"))
		{
			print ("TeleHit");
			Application.LoadLevel("BlueLevel");
		}
	}
}