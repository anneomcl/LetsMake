using UnityEngine;
using System.Collections;

public class YellowTeleport : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			SceneManager.LoadSceneAsync("YellowLevel");
		}
	}
}