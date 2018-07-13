using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour {

	public GameObject ObjectToInstantiate;

	private Player player;
	private SlotCounter slotCounter;
	private Compile compile;

	private GameObject button;
	private int currentChild = 0;
	private float delay;


	void Start(){
		player = FindObjectOfType<Player> ().GetComponent<Player> ();
		slotCounter = FindObjectOfType<SlotCounter> ().GetComponent<SlotCounter> ();
		compile = FindObjectOfType<Compile> ().GetComponent<Compile> ();
	}

	// Recieves instructions from buttons & add them to the editor
	public void AddInstructions(string instruction){
		GameObject InventorySlot = Instantiate (ObjectToInstantiate, transform) as GameObject;
		Text TextComponent = InventorySlot.transform.GetChild (0).GetComponentInChildren<Text> ();
		TextComponent.text = instruction;

		slotCounter.GenerateNumbering (true);
	}

	// executes all the available instructions in the editor sequentially with desired delay in between
	public void ExecuteInstructions(float delay){
		this.delay = delay;
//		print (transform.childCount);
		currentChild = 0;
		InvokeRepeating ("SingleInstruction",1,delay);
	}

	// all new instructions have to be added here (inside the switch case) before testing/Executing to ensure desired working.
	// responsible with reading the current instruction & communicating it with the Player script.
	void SingleInstruction(){
		if (currentChild < transform.childCount) {
			button = transform.GetChild (currentChild).GetChild (0).gameObject;
			StartCoroutine ("ColorSwitchDelay");
			Text instruction = button.GetComponentInChildren<Text> ();
			switch(instruction.text){
			case "MoveForward()":
				player.MoveForward ();
				break;
			case "TurnLeft()":
				player.TurnLeft ();
				break;
			case "TurnRight()":
				player.TurnRight ();
				break;
			case "For() {  ":

				break;
			default:
				Debug.LogError (gameObject.name + "instruction not available");
				break;
			}
			currentChild++;
		} else {
			StopExecution ();
		}
	}

	public void StopExecution(){
		CancelInvoke ("SingleInstruction");
		compile.EnableButton ();
	}

	// to indicate if the Instruction is currently in execution or not.
	void SetExecutionColor(bool value){
		Image toChangeColor = button.GetComponent<Image> ();
		if(value){
			toChangeColor.color = Color.red;
		}else{
			toChangeColor.color = Color.white;
		}
	}

	// cannot be >= "delay" seconds as object instance would get changed
	IEnumerator ColorSwitchDelay(){
		SetExecutionColor (true);
		yield return new WaitForSeconds (delay - 0.5f);
		SetExecutionColor (false);
	}

	// removes all the instructions added in the editor
	public void RemoveAllInstructions(){
		foreach(InventorySlot i in transform.GetComponentsInChildren<InventorySlot>()){
			i.RemoveSlot ();
		}
	}

	// remove a single instruction slot passed as a parameter
	public void RemoveInstruction(GameObject toDestroy){
		Destroy (toDestroy.gameObject);
		slotCounter.GenerateNumbering (false);
	}
}