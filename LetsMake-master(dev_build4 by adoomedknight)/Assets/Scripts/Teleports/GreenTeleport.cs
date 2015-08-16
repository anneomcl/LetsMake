using UnityEngine;
using System.Collections;

public class GreenTeleport : MonoBehaviour {



	void OnTriggerEnter(Collider other)
	{


		if(other.gameObject.CompareTag("Player"))
		{
			print ("TeleHit");
			Application.LoadLevel("GreenLevel");
		}
	}
}