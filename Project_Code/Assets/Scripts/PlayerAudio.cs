using UnityEngine;

public class PlayerAudio : MonoBehaviour {

	public AudioClip[] audioClipArray;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlayAudioFor(string clipName){

		switch(clipName){
		case "MoveForward()":
			audioSource.clip = audioClipArray [0];
			break;
		case "TurnLeft()":
			audioSource.clip = audioClipArray [1];
			break;
		case "TurnRight()":
			audioSource.clip = audioClipArray [2];
			break;
		default:
			Debug.LogError (gameObject.name + "instruction not available");
			break;
		}
		audioSource.Play ();
	}
}
