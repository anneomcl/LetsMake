using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour {

	public float Timer;
	public TextMesh TimerTxt;
	public TextMesh TutorialText;
	public TextMesh BewareZombies;

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	
		//Timer in minutes and seconds.
		Timer += Time.smoothDeltaTime;
		int minutes = (int)Timer / 60;
		int seconds = (int)Timer % 60;

		//Update the timer text.
		TimerTxt.text = "" + minutes.ToString() + ":" + seconds.ToString("00");

		if (Timer >= 5) {
			//Destroy(TutorialText);
		}
		if (Timer >= 3) {
			Destroy (BewareZombies);
		}
	}
}
