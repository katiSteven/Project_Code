using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCounter : MonoBehaviour {

	public GameObject ObjectToInstantiate;

	private SlotManager slotManager;

	// Use this for initialization
	void Start () {
		slotManager = FindObjectOfType<SlotManager> ();

		GenerateNumbering (true);
	}

	public void AddNumber(int number){
		GameObject InventorySlot = Instantiate (ObjectToInstantiate, transform) as GameObject;
		Text TextComponent = InventorySlot.GetComponentInChildren<Text> ();
		TextComponent.text = number.ToString ();
	}

	// rework this class, try & implement foreach loop to check the count of inventory slots
	// rework number generation to check every frame in the Update function.

	public void GenerateNumbering(bool isAddingInstructions){
		
		RemoveNumbering ();


		if (isAddingInstructions) {
			for (int i = 0; i < GetInstructionCount (true); i++) {
				AddNumber (i + 1);
			}
		} else {
			for (int i = 0; i < GetInstructionCount (false); i++) {
				AddNumber (i + 1);
			}
		}
	}

	int GetInstructionCount(bool isAddingInstructions){

		List<InventorySlot> total = new List<InventorySlot> ();

		// To get all the children 
		slotManager.GetComponentsInChildren<InventorySlot>(total);
		if (isAddingInstructions) {
			return total.Count;
		} else {
			return total.Count - 1;
		}
	}

	// Remove previously assigned numbering
	void RemoveNumbering(){
		foreach(NumberIndicator nid in GetComponentsInChildren<NumberIndicator>()){
			nid.DestroySelf ();
		}
	}
}
