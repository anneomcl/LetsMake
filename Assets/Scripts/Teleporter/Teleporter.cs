using UnityEngine;
using System;
using System.Collections;

//	The teleporter has a Mode enum instead of creating a teleporter class hierarchy where each teleporter type is
//	derived from a base class. The reason for this is that if you want to change the teleporter mode you only have to 
//	change a variable in the inspector. With a class hierarchy you'd need to remove the component and replace it with another
//	teleporter component which could break references in the scene or in prefabs.
public class Teleporter : MonoBehaviour
{
	[SerializeField]
	private TeleportMode teleportMode;

	[SerializeField]
	private string sceneName;

	[SerializeField]
	private Teleporter otherTeleporter;

	[SerializeField]
	private Transform spawnPoint;

	private bool isExpectingPlayer = false;

	private void OnTriggerEnter (Collider other) {
		if (other.tag == "Player" && !isExpectingPlayer) {
			if (teleportMode == TeleportMode.Scene) {
				if (!string.IsNullOrEmpty(sceneName))
					SceneManager.LoadSceneAsync(sceneName);
				else 
					Debug.LogErrorFormat("Teleporter {0} could not be activated. Scene name is not valid.", gameObject.name);
			} else {
				if (otherTeleporter != null)
					otherTeleporter.ReceivePlayer(other.gameObject);
				else
					Debug.LogErrorFormat("Teleporter {0} could not be activated. Linked teleporter is null.", gameObject.name);
			}
		}
	}

	private void OnTriggerExit (Collider other) {
		if (other.tag == "Player")
			isExpectingPlayer = false;
	}

	public void ReceivePlayer (GameObject player) {
		if (player != null && !isExpectingPlayer) {
			isExpectingPlayer = true;
			player.transform.position = spawnPoint.position;
		}
	}
}