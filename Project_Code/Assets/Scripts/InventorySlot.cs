using UnityEngine;

// has functionalities which are unique to individual inventory slot
public class InventorySlot : MonoBehaviour {

	private SlotManager slotManager;
	private UI_Audio ui_Audio;

	void Start(){
		slotManager = FindObjectOfType<SlotManager> ().GetComponent<SlotManager> ();
		ui_Audio = FindObjectOfType<UI_Audio> ().GetComponent<UI_Audio> ();
	}
	
	public void RemoveSlot(){
		ui_Audio.PlayAudioFor ("clearButton");
		slotManager.RemoveInstruction (gameObject);
	}
}