using UnityEngine;
using System.Collections;

public class PurpleTeleport : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			SceneManager.LoadSceneAsync("PurpleLevel");
		}
	}
}