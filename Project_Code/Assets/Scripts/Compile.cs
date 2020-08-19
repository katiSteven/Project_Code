using UnityEngine;
using UnityEngine.UI;

public class Compile : MonoBehaviour {

	public float instructionDelay;
    public Button Forwardbutton;
    public Button Leftbutton;
    public Button Rightbutton;
    public Button RemoveAllInstructionsbutton;

    private SlotManager slotManager;
	private Scrollbar scrollbar;
	private Player player;
	private Button Playbutton;

	void Start () {
		slotManager = FindObjectOfType<SlotManager> ();
		scrollbar = FindObjectOfType<Scrollbar> ();
		player = FindObjectOfType<Player> ();
		Playbutton = GetComponent<Button> ();
	}

    //This method gets called when the Plat Button is pressed
	public void CompileInstructions(){
		
		player.SetToPosition (player.GetInitialPosition ());
		player.SetToRotation (player.GetInitialRotation ());

		scrollbar.value = 1f;	//go to the top of the editor i.e to the first instruction

		DisableButtons ();
		slotManager.ExecuteInstructions (instructionDelay);	//execute all instructions with visual delay after every instruction
	}

    //enable buttons after excution
	public void EnableButtons(){
		Playbutton.interactable = true;
        Forwardbutton.interactable = true;
        Leftbutton.interactable = true;
        Rightbutton.interactable = true;
        RemoveAllInstructionsbutton.interactable = true;

        for (int i = 0; i < slotManager.transform.childCount; i++) {
            Button removeButtonsInSlots = slotManager.transform.GetChild(i).GetChild(1).GetComponent<Button>();
            removeButtonsInSlots.interactable = true;
        }
    }

    //disable buttons while execution
	void DisableButtons(){
		Playbutton.interactable = false;
        Forwardbutton.interactable = false;
        Leftbutton.interactable = false;
        Rightbutton.interactable = false;
        RemoveAllInstructionsbutton.interactable = false;
    }

    //big cross button -> removes all instructions
	public void RemoveInstructions(){
		slotManager.RemoveAllInstructions ();
	}
}
