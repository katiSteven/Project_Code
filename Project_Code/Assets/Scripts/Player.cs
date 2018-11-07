using UnityEngine;

public enum MDIRECTION {FORWARD, BACKWARD, LEFT, RIGHT};

public class Player : MonoBehaviour {

	public int speed = 10;
	public float distanceToCover = 1f;
	public MDIRECTION direction = MDIRECTION.FORWARD;

	private PlayerAudio playerAudio;
	private bool canMove = true, moving = false;
	private Vector3 nextPosition;
	private MDIRECTION initialDirection = MDIRECTION.FORWARD;
	private Vector3 initialPosition;
	private bool isColliding = false;

	void Awake () {
		//initial
		SetInitialPosition (transform.position);
		SetInitialRotation (direction);

		//current
		//SetToPosition (transform.position);   //dont enable causing the jumping issue
		SetToRotation (direction);
	}

	void Start () {
		playerAudio = GetComponent<PlayerAudio> ();
	}

    void SetInitialPosition (Vector3 position){
		initialPosition = position;
	}

    void SetInitialRotation (MDIRECTION dir){
		initialDirection = dir;
	}

    public void SetToPosition (Vector3 position){
		nextPosition = position;
		transform.position = position;
	}

    public void SetToRotation (MDIRECTION dir){
		direction = dir;
		transform.rotation = Quaternion.AngleAxis (0, Vector3.up);	//default rotation

		//to assign an initial direction to the character, use this  instead of transform->Rotation
		switch(dir){
		case MDIRECTION.FORWARD:
			transform.rotation *= Quaternion.AngleAxis (0, Vector3.up);
			break;
		case MDIRECTION.LEFT:
			transform.rotation *= Quaternion.AngleAxis (-90f, Vector3.up);
			break;
		case MDIRECTION.BACKWARD:
			transform.rotation *= Quaternion.AngleAxis (180f, Vector3.up);
			break;
		case MDIRECTION.RIGHT:
			transform.rotation *= Quaternion.AngleAxis (90f, Vector3.up);
			break;
		}
	}

	public Vector3 GetInitialPosition (){ return initialPosition; }

	public MDIRECTION GetInitialRotation (){ return initialDirection; }

	void Update () {

        if (canMove) { nextPosition = transform.position; }

        if (moving){
			if(transform.position == nextPosition){
				moving = false;
				canMove = true;
			}
			transform.position = Vector3.MoveTowards (transform.position, nextPosition, Time.deltaTime * speed);
		}
	}

	void OnCollisionStay(Collision col){
		if (col.gameObject.GetComponent<MeshFilter> ()) {
			isColliding = true;
		}
	}

	void OnCollisionExit(){ isColliding = false; }

	public bool OnGround(){
		if (isColliding)
			return true;
        return false;
	}

	public void MoveForward(){
		playerAudio.PlayAudioFor ("MoveForward()");

		canMove = false;
		moving = true;

        //moves according to the 
		switch(direction){
		case MDIRECTION.FORWARD:
			nextPosition += Vector3.forward * distanceToCover;
			break;
		case MDIRECTION.LEFT:
			nextPosition += Vector3.left * distanceToCover;
			break;
		case MDIRECTION.BACKWARD:
			nextPosition += Vector3.back * distanceToCover;
			break;
		case MDIRECTION.RIGHT:
			nextPosition += Vector3.right * distanceToCover;
			break;
		}

	}

	public void TurnLeft(){
		playerAudio.PlayAudioFor ("TurnLeft()");

		canMove = false;
		moving = true;

		transform.rotation *= Quaternion.AngleAxis (-90f, Vector3.up);  //this rotates the model

        //changes the enum direction for the upcoming instructions
        switch (direction){
		case MDIRECTION.FORWARD:
			direction = MDIRECTION.LEFT;
			break;
		case MDIRECTION.LEFT:
			direction = MDIRECTION.BACKWARD;
			break;
		case MDIRECTION.BACKWARD:
			direction = MDIRECTION.RIGHT;
			break;
		case MDIRECTION.RIGHT:
			direction = MDIRECTION.FORWARD;
			break;
		}
	}

	public void TurnRight(){
		playerAudio.PlayAudioFor ("TurnRight()");

		canMove = false;
		moving = true;

        transform.rotation *= Quaternion.AngleAxis(90f, Vector3.up);	//this rotation is purely for visual representation

		switch(direction){
		case MDIRECTION.FORWARD:
			direction = MDIRECTION.RIGHT;
			break;
		case MDIRECTION.LEFT:
			direction = MDIRECTION.FORWARD;
			break;
		case MDIRECTION.BACKWARD:
			direction = MDIRECTION.LEFT;
			break;
		case MDIRECTION.RIGHT:
			direction = MDIRECTION.BACKWARD;
			break;
		}
	}
}