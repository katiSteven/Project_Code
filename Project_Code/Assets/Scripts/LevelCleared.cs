using System.Collections;
using UnityEngine;

public class LevelCleared : MonoBehaviour {

	public string NextLevelName;

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ().GetComponent<LevelManager> ();
	}

	IEnumerator LevelLoadAfterDelay(){
		yield return new WaitForSeconds (1f);
		levelManager.LoadLevel (NextLevelName);
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.GetComponent<Player>()){
			StartCoroutine ("LevelLoadAfterDelay");
		}
	}
}
