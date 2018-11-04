using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCounter : MonoBehaviour {

	public GameObject ObjectToInstantiate;

	private SlotManager slotManager;

	// Use this for initialization
	void Start () {
		slotManager = FindObjectOfType<SlotManager> ();

		GenerateNumbering ();
	}

	public void AddNumber(int number){
		GameObject InventorySlot = Instantiate (ObjectToInstantiate, transform) as GameObject;
		Text TextComponent = InventorySlot.GetComponentInChildren<Text> ();
        TextComponent.text = number.ToString();
    }

    // rework this class, try & implement foreach loop to check the count of inventory slots
    // rework number generation to check every frame in the Update function.

    public void GenerateNumbering(){
		RemoveNumbering ();

        int childCount = slotManager.transform.childCount;
        for (int i = 0; i < childCount; i++)
        { AddNumber(i + 1); }
    }

	// Remove previously assigned numbering
	public void RemoveNumbering(){
		foreach(NumberIndicator nid in GetComponentsInChildren<NumberIndicator>()){
			nid.DestroySelf ();
		}
	}
}