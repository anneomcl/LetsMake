using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public void StartGameAsGirl()
	{	
		PlayerPrefs.SetString("gender", "girl");
		PlayerPrefs.Save();
		StartGame ();
	}

	public void StartGameAsBoy()
	{
		PlayerPrefs.SetString("gender", "boy");
		PlayerPrefs.Save();
		StartGame ();
	}

	public void StartGame() {
		Application.LoadLevel("HelloWorld");
	}
}
