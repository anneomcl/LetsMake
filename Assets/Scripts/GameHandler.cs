using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {

		//So you can quit from the game.
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}

	}
}
