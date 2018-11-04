using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// has functionalities & text which are unique to individual inventory slot
public class InventorySlot : MonoBehaviour {

	private SlotManager slotManager;
	private UI_Audio ui_Audio;

	void Start(){
		slotManager = FindObjectOfType<SlotManager> ().GetComponent<SlotManager> ();
		ui_Audio = FindObjectOfType<UI_Audio> ().GetComponent<UI_Audio> ();
	}
	
	public void RemoveSlot(){
		ui_Audio.PlayAudioFor ("clearButton");
		Text textComponent = transform.GetChild (0).GetComponentInChildren<Text> ();
		string textInside = textComponent.text;

		int groupedInstructionCount = int.Parse( new string(textInside.Where(char.IsDigit).ToArray()));

		if(groupedInstructionCount <= 1){
			slotManager.RemoveInstruction (gameObject);
		}

		if (textInside.Contains ("MoveForward")) {
			textComponent.text = "MoveForward" + "(" + (--groupedInstructionCount) + ")";
		}else if(textInside.Contains ("TurnLeft")) {
			textComponent.text = "TurnLeft" + "(" + (--groupedInstructionCount) + ")";
		}else if(textInside.Contains ("TurnRight")){
			textComponent.text = "TurnRight" + "(" + (--groupedInstructionCount) + ")";
		}
	}
}