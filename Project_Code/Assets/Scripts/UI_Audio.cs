using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Audio : MonoBehaviour {

	public AudioClip playButton, clearButton, restartButton, closeLevelButton;
	public AudioClip[] playerControls;

	private AudioSource audioSource;
	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlayAudioFor(string clipName){

		switch(clipName){
		case "MoveForward()":
			audioSource.clip = playerControls [0];
			break;
		case "TurnLeft()":
			audioSource.clip = playerControls [1];
			break;
		case "TurnRight()":
			audioSource.clip = playerControls [2];
			break;
		case "PlayButton":
			audioSource.clip = playButton;
			break;
		case "clearButton":
			audioSource.clip = clearButton;
			break;
		case "restartButton":
			audioSource.clip = restartButton;
			break;
		case "closeLevelButton":
			audioSource.clip = closeLevelButton;
			break;
		default:
			Debug.LogError (gameObject.name + "instruction not available");
			break;
		}
		audioSource.Play ();
	}
}