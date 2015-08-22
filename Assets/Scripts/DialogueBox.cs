using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DisplayText (string text) {
		GetComponentInChildren<Text>().text = text;
		Invoke ("HideText", 1.0f + text.Length / 25f);
	}

	void HideText () {
		gameObject.SetActive(false);
	}
}
