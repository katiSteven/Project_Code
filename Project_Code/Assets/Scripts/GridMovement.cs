using UnityEngine;

public enum DIRECTION { FORWARD, BACKWARD, LEFT, RIGHT};

public class GridMovement : MonoBehaviour {

	private bool canMove = true, moving = false;
	private int speed = 5, buttonCoolDown = 0;
	private DIRECTION dir = DIRECTION.BACKWARD;
	private Vector3 pos;

	// Update is called once per frame
	void Update () {
		buttonCoolDown--;

		if (canMove) {
			pos = transform.position;
		}

		if(moving){
			if(transform.position == pos){
				moving = false;
				canMove = true;
			}
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);
		}
	}

	public void MoveForward(){
		if (buttonCoolDown <= 0) {
			if (dir != DIRECTION.FORWARD) {
				buttonCoolDown = 5;
				dir = DIRECTION.FORWARD;
			} else {
				canMove = false;
				moving = true;
				pos += Vector3.forward;
			}
		}
	}

	public void MoveBackward(){
		if (buttonCoolDown <= 0) {
			if (dir != DIRECTION.BACKWARD) {
				buttonCoolDown = 5;
				dir = DIRECTION.BACKWARD;
			} else {
				canMove = false;
				moving = true;
				pos += Vector3.back;
			}
		}
	}

	public void MoveLeft(){
		if (buttonCoolDown <= 0) {
			if (dir != DIRECTION.LEFT) {
				buttonCoolDown = 5;
				dir = DIRECTION.LEFT;
			} else {
				canMove = false;
				moving = true;
				pos += Vector3.left;
			}
		}
	}

	public void MoveRight(){
		if (buttonCoolDown <= 0) {
			if (dir != DIRECTION.RIGHT) {
				buttonCoolDown = 5;
				dir = DIRECTION.RIGHT;
			} else {
				canMove = false;
				moving = true;
				pos += Vector3.right;
			}
		}
	}
//	private void Move(){
//		if(buttonCoolDown <= 0){
//			if(Input.GetKeyDown(KeyCode.UpArrow)){
//				if (dir != DIRECTION.FORWARD) {
//					buttonCoolDown = 5;
//					dir = DIRECTION.FORWARD;
//				} else {
//					canMove = false;
//					moving = true;
//					pos += Vector3.forward;
//				}
//			}else if(Input.GetKeyDown(KeyCode.DownArrow)){
//				if (dir != DIRECTION.BACKWARD) {
//					buttonCoolDown = 5;
//					dir = DIRECTION.BACKWARD;
//				} else {
//					canMove = false;
//					moving = true;
//					pos += Vector3.back;
//				}
//			}else if(Input.GetKeyDown(KeyCode.LeftArrow)){
//				if (dir != DIRECTION.LEFT) {
//					buttonCoolDown = 5;
//					dir = DIRECTION.LEFT;
//				} else {
//					canMove = false;
//					moving = true;
//					pos += Vector3.left;
//				}
//			}else if(Input.GetKeyDown(KeyCode.RightArrow)){
//				if (dir != DIRECTION.RIGHT) {
//					buttonCoolDown = 5;
//					dir = DIRECTION.RIGHT;
//				} else {
//					canMove = false;
//					moving = true;
//					pos += Vector3.right;
//				}
//			}
//		}
//	}
}