using UnityEngine;
using System.Collections;

public class DialogAudio : MonoBehaviour {

	public double dialogDuration = 2;
	public AudioClip[] soundClips;

	private double elapsedTime = 0;
	private bool playing = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (playing) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= dialogDuration){
				playing = false;
				elapsedTime = 0;
			}
		}
	}

	public bool startDialog() {
		if (playing) {
			return false; // Audio is already playing
		}

		playing = true;
		PlayDialogClip(soundClips[Random.Range(0, soundClips.Length-1)], DialogClipFinished);

		return true;
	}

	public delegate void AudioCallback();
	private void PlayDialogClip(AudioClip clip, AudioCallback callback) {
		AudioSource.PlayClipAtPoint (clip, transform.position);
		StartCoroutine (DelayedCallback (clip.length, callback));
	}

	private IEnumerator DelayedCallback(float time, AudioCallback callback) {
		yield return new WaitForSeconds(time);
		callback();
	}
	
	private void DialogClipFinished() {
		if (playing) {
			PlayDialogClip(soundClips[Random.Range(0, soundClips.Length-1)], DialogClipFinished);
		}
	}
		          
}
