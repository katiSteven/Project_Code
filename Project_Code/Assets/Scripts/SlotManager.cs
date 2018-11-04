using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class SlotManager : MonoBehaviour {

	public GameObject ObjectToInstantiate;

    Queue<string> instructionQueue = new Queue<string>();
    Queue<int> groupedNumberingQueue = new Queue<int>();

    private Player player;
	private SlotCounter slotCounter;
	private Compile compile;
	private GameObject button;
	private GameObject InventorySlot;
	private float delay;
	private string previousInstruction;
	private int groupedInstructionCount = 0;

	void Start(){
		player = FindObjectOfType<Player> ().GetComponent<Player> ();
		slotCounter = FindObjectOfType<SlotCounter> ().GetComponent<SlotCounter> ();
		compile = FindObjectOfType<Compile> ().GetComponent<Compile> ();
	}

	// Recieves instructions from buttons & add them to the editor
	public void AddInstructions(string instruction){
		if (instruction.Equals (previousInstruction)) {
			Text textComponent = InventorySlot.transform.GetChild (0).GetComponentInChildren<Text> ();
			string textInside = textComponent.text;
			int previousNumCount = int.Parse( new string(textInside.Where(char.IsDigit).ToArray()));

			textComponent.text = instruction + "(" + (++previousNumCount) + ")";
		} else {
			InventorySlot = Instantiate (ObjectToInstantiate, transform) as GameObject;
			Text textComponent = InventorySlot.transform.GetChild (0).GetComponentInChildren<Text> ();
			previousInstruction = instruction;
			textComponent.text = instruction + "(" + (1) + ")";
		}

		//below code generates the Line numbering in the instruction editor GameObject
		slotCounter.GenerateNumbering ();
	}

	// executes all the available instructions in the editor sequentially with desired delay in between
	public void ExecuteInstructions(float delay){
		this.delay = delay;
		GenerateSingleInstruction ();
		InvokeRepeating ("SingleInstruction",1,delay);
	}

	// all new instructions have to be added here (inside the switch case) before testing/Executing to ensure desired working.
	// responsible with reading the current instruction & communicating it with the Player script.
	void SingleInstruction(){
		if (instructionQueue.Count > 0) {
            if (player.OnGround()) {
                string instruction = instructionQueue.Peek();
                instructionQueue.Dequeue();
                if (instruction.Equals("MoveForward"))
                { player.MoveForward(); }
                else if (instruction.Equals("TurnLeft"))
                { player.TurnLeft(); }
                else if (instruction.Equals("TurnRight"))
                { player.TurnRight(); }
                else { Debug.LogError(instruction + " is not a valid instruction"); }
            }
		} else { StopExecution (); }
	}

	public void StopExecution(){
		CancelInvoke ("SingleInstruction");
		instructionQueue.Clear ();
		compile.EnableButton ();
	}

	void GenerateSingleInstruction(){
		for(int i = 0; i < transform.childCount; i++){
			button = transform.GetChild (i).GetChild (0).gameObject;
			Text instruction = button.GetComponentInChildren<Text> ();

			string textInside = instruction.text;
			// to get the number value out of an instruction
			groupedInstructionCount = int.Parse(new string(textInside.Where(char.IsDigit).ToArray()));

			//grouped number inside every instruction line
			groupedNumberingQueue.Enqueue (groupedInstructionCount);	//make use of this to color the currently executing statement

			//the below for loop converts for eg: "Move(3)" to Move(), Move(), Move().
			for (int j = 0; j < groupedInstructionCount; j++) {
				if (textInside.Contains ("MoveForward")) {
					instructionQueue.Enqueue ("MoveForward");
				} else if (textInside.Contains ("TurnLeft")) {
					instructionQueue.Enqueue ("TurnLeft");
				} else if (textInside.Contains ("TurnRight")) {
					instructionQueue.Enqueue ("TurnRight");
				} else {
					Debug.LogError (textInside + " is not a valid instruction");
				}
			}
		}
	}

	// to indicate if the Instruction is currently in execution or not.
	void SetExecutionColor(bool value){
		Image toChangeColor = button.GetComponent<Image> ();
		if(value) { toChangeColor.color = Color.red; }
        else { toChangeColor.color = Color.white; }
	}

	// cannot be >= "delay" seconds as object instance would get changed
	IEnumerator ColorSwitchDelay(){
		SetExecutionColor (true);
		yield return new WaitForSeconds (delay - 0.5f);
		SetExecutionColor (false);
	}

	// removes all the instructions added in the editor
	public void RemoveAllInstructions(){
		foreach(InventorySlot i in GetComponentsInChildren<InventorySlot>()){
			Destroy(i.gameObject);
        }
        slotCounter.RemoveNumbering();
        previousInstruction = null;
    }

	// remove a single instruction slot passed as a parameter
	public void RemoveInstruction(GameObject toDestroy){
		DestroyImmediate (toDestroy.gameObject);
		slotCounter.GenerateNumbering ();
        int childCount = transform.childCount;
        if (childCount > 0)
        {
            GameObject aboveInventorySlot = transform.GetChild(childCount - 1).gameObject;
            Text lastInstructionText = aboveInventorySlot.transform.GetChild(0).GetComponentInChildren<Text>();
            string textInside = lastInstructionText.text;

            InventorySlot = aboveInventorySlot;

            if (textInside.Contains("MoveForward"))
            { previousInstruction = "MoveForward"; }
            else if (textInside.Contains("TurnLeft"))
            { previousInstruction = "TurnLeft"; }
            else if (textInside.Contains("TurnRight"))
            { previousInstruction = "TurnRight"; }
            else { Debug.LogError(textInside + " is not a valid instruction"); }
        }
        else { previousInstruction = null; }
    }
}