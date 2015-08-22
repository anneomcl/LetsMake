using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour 
{
	public string gameScene = "MazeRoom_01";

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

	public void StartGame() 
	{
		SceneManager.LoadSceneAsync(gameScene);
	}
}
