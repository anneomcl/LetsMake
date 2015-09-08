using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBox : MonoBehaviour {

	void DisplayText (string text) {
		GetComponentInChildren<Text>().text = text;
		Invoke("HideText", 1.0f + text.Length / 25f);
	}

	void HideText () {
		gameObject.SetActive(false);
	}
}
