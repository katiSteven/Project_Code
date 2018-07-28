using UnityEngine;

//all new instructions have to be added here as Functions before testing
public class PlayerControls : MonoBehaviour {

	private SlotManager slotManager;

	// Use this for initialization
	void Start () {
		slotManager = GameObject.FindObjectOfType<SlotManager> ();
	}

	public void MoveForward(){
		slotManager.AddInstructions ("MoveForward");	//function paranthesis will be provided by
														// the SlotManager function
	}

	public void MoveLeft(){
		slotManager.AddInstructions ("TurnLeft");
	}

	public void MoveRight(){
		slotManager.AddInstructions ("TurnRight");
	}
}

// groupedInstructionCount = int.Parse( new string(textInside.Where(char.IsDigit).ToArray()));

//button = transform.GetChild (currentChild).GetChild (0).gameObject;
//Text instruction = button.GetComponentInChildren<Text> ();
//StartCoroutine ("ColorSwitchDelay");

//after evecuting an instruction
//currentChild++;