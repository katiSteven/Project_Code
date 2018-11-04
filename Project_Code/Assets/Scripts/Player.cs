using UnityEngine;

public enum LDIRECTION {FORWARD, BACKWARD, LEFT, RIGHT};

public class Player : MonoBehaviour {

	public int speed = 10;
	public float distanceToCover = 1f;
	public LDIRECTION direction = LDIRECTION.FORWARD;

	private PlayerAudio playerAudio;
	private readonly Collider collider1 = new Collider();
	private bool canMove = true, moving = false;
	private Vector3 nextPosition;
	private LDIRECTION initialDirection = LDIRECTION.FORWARD;
	private Vector3 initialPosition;
	private bool isColliding = false;

	void Awake () {
		//initial
		SetInitialPosition (transform.position);
		SetInitialRotation (direction);

		//current
		SetToPosition (transform.position);
		SetToRotation (direction);
	}

	void Start () {
		playerAudio = GetComponent<PlayerAudio> ();
	}

    void SetInitialPosition (Vector3 position){
		initialPosition = position;
	}

    void SetInitialRotation (LDIRECTION dir){
		initialDirection = dir;
	}

    public void SetToPosition (Vector3 position){
		nextPosition = position;
		transform.position = position;
	}

    public void SetToRotation (LDIRECTION dir){
		direction = dir;
		transform.rotation = Quaternion.AngleAxis (0, Vector3.up);	//default rotation

		//to assign an initial direction to the character, use this  instead of transform->Rotation
		switch(dir){
		case LDIRECTION.FORWARD:
			transform.rotation *= Quaternion.AngleAxis (0, Vector3.up);
			break;
		case LDIRECTION.LEFT:
			transform.rotation *= Quaternion.AngleAxis (-90f, Vector3.up);
			break;
		case LDIRECTION.BACKWARD:
			transform.rotation *= Quaternion.AngleAxis (180f, Vector3.up);
			break;
		case LDIRECTION.RIGHT:
			transform.rotation *= Quaternion.AngleAxis (90f, Vector3.up);
			break;
		}
	}

	public Vector3 GetInitialPosition (){ return initialPosition; }

	public LDIRECTION GetInitialRotation (){ return initialDirection; }

	void Update () {

		if (canMove) { nextPosition = transform.position; }

		if(moving){
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
		else
			return false;
	}

	public void MoveForward(){
		playerAudio.PlayAudioFor ("MoveForward()");

		canMove = false;
		moving = true;

        //moves according to the 
		switch(direction){
		case LDIRECTION.FORWARD:
			nextPosition += Vector3.forward * distanceToCover;
			break;
		case LDIRECTION.LEFT:
			nextPosition += Vector3.left * distanceToCover;
			break;
		case LDIRECTION.BACKWARD:
			nextPosition += Vector3.back * distanceToCover;
			break;
		case LDIRECTION.RIGHT:
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
		case LDIRECTION.FORWARD:
			direction = LDIRECTION.LEFT;
			break;
		case LDIRECTION.LEFT:
			direction = LDIRECTION.BACKWARD;
			break;
		case LDIRECTION.BACKWARD:
			direction = LDIRECTION.RIGHT;
			break;
		case LDIRECTION.RIGHT:
			direction = LDIRECTION.FORWARD;
			break;
		}
	}

	public void TurnRight(){
		playerAudio.PlayAudioFor ("TurnRight()");

		canMove = false;
		moving = true;

        transform.rotation *= Quaternion.AngleAxis(90f, Vector3.up);	//this rotation is purely for visual representation

		switch(direction){
		case LDIRECTION.FORWARD:
			direction = LDIRECTION.RIGHT;
			break;
		case LDIRECTION.LEFT:
			direction = LDIRECTION.FORWARD;
			break;
		case LDIRECTION.BACKWARD:
			direction = LDIRECTION.LEFT;
			break;
		case LDIRECTION.RIGHT:
			direction = LDIRECTION.BACKWARD;
			break;
		}
	}
}