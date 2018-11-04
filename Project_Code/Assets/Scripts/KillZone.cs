using System.Collections;
using UnityEngine;

public class KillZone : MonoBehaviour {

	private SlotManager slotManager;
	private Player player;
	private Material material;

	// Use this for initialization
	void Start () {
		slotManager = FindObjectOfType<SlotManager> ();
		player = FindObjectOfType<Player> ();
		material = GetComponent<Renderer> ().material;
		material.color = Color.white;
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.GetComponent<Player>()){
			player.SetToPosition(player.GetInitialPosition () + new Vector3(0,3f,0));
			player.SetToRotation(player.GetInitialRotation ());
			StartCoroutine ("KillZoneColorDelay");
			slotManager.StopExecution ();
		}
	}

	IEnumerator KillZoneColorDelay(){
		material.color = Color.red;
		yield return new WaitForSeconds (2f);
		material.color = Color.white;
		StopCoroutine ("KillZoneColorDelay");
	}
}