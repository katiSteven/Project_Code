using UnityEngine;
using UnityEngine.UI;

public class Compile : MonoBehaviour {

	public float instructionDelay;

	private SlotManager slotManager;
	private Scrollbar scrollbar;
	private Player player;
	private Button button;

	void Start () {
		slotManager = FindObjectOfType<SlotManager> ();
		scrollbar = FindObjectOfType<Scrollbar> ();
		player = FindObjectOfType<Player> ();
		button = GetComponent<Button> ();
	}

	public void CompileInstructions(){
		
		player.SetToPosition (player.GetInitialPosition ());
		player.SetToRotation (player.GetInitialRotation ());

		scrollbar.value = 1f;	//go to the top of the editor i.e to the first instruction

		DisableButton ();
		slotManager.ExecuteInstructions (instructionDelay);	//execute all instructions with visual delay after every instruction
	}

	public void EnableButton(){
		button.interactable = true;
	}

	void DisableButton(){
		button.interactable = false;
	}

	public void RemoveInstructions(){
		slotManager.RemoveAllInstructions ();
	}
}
