using UnityEngine;

public enum LDIRECTION {FORWARD, BACKWARD, LEFT, RIGHT};

public class Player : MonoBehaviour {

	public int speed = 10;
	public float distanceToCover = 1f;
	public LDIRECTION dir = LDIRECTION.FORWARD;

	private PlayerAudio playerAudio;

	private bool canMove = true, moving = false;

	private Vector3 pos;								//holds the next posiiton to move towards
	private LDIRECTION initDir = LDIRECTION.FORWARD;	//stores initial direction
	private Vector3 initPos;							//stores initial position

	// Use this for initialization
	void Awake () {
		//initial
		SetInitialPosition (transform.position);
		SetInitialRotation (dir);

		//current
		SetToPosition (transform.position);
		SetToRotation (dir);
	}

	// Use this for initialization
	void Start () {
		playerAudio = GetComponent<PlayerAudio> ();
	}
		
	void OnCollisionEnter(Collision col){

		// making the green start platform as the initial position
		if (col.gameObject.CompareTag ("StartPlatform")) {
			SetInitialPosition (transform.position);
		}
	}

	void SetInitialPosition (Vector3 position){
		initPos = position;		//using this method will discard the previous initial position of the gameObject, use with caution
	}

	void SetInitialRotation (LDIRECTION dir){
		initDir = dir;			//using this method will discard the previous initial position of the gameObject, use with caution
	}

	public void SetToPosition (Vector3 position){
		//		initPos = position;		//using this method will discard the previous initial position of the gameObject, use with caution
		pos = position;
		transform.position = position;
	}

	public void SetToRotation (LDIRECTION dir){
		//		initDir = dir;			//using this method will discard the previous initial position of the gameObject, use with caution
		this.dir = dir;
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

	public Vector3 GetInitialPosition (){
		return initPos;
	}

	public LDIRECTION GetInitialRotation (){
		return initDir;
	}

	// Update is called once per frame
	void Update () {

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
		playerAudio.PlayAudioFor ("MoveForward()");

		canMove = false;
		moving = true;
		switch(dir){
		case LDIRECTION.FORWARD:
			pos += Vector3.forward * distanceToCover;
			break;
		case LDIRECTION.LEFT:
			pos += Vector3.left * distanceToCover;
			break;
		case LDIRECTION.BACKWARD:
			pos += Vector3.back * distanceToCover;
			break;
		case LDIRECTION.RIGHT:
			pos += Vector3.right * distanceToCover;
			break;
		}

	}

	public void TurnLeft(){
		playerAudio.PlayAudioFor ("TurnLeft()");

		canMove = false;
		moving = true;
		transform.rotation *= Quaternion.AngleAxis (-90f, Vector3.up);	//this rotation is purely for visual representation
		switch(dir){
		case LDIRECTION.FORWARD:
			dir = LDIRECTION.LEFT;
			break;
		case LDIRECTION.LEFT:
			dir = LDIRECTION.BACKWARD;
			break;
		case LDIRECTION.BACKWARD:
			dir = LDIRECTION.RIGHT;
			break;
		case LDIRECTION.RIGHT:
			dir = LDIRECTION.FORWARD;
			break;
		}
	}

	public void TurnRight(){
		playerAudio.PlayAudioFor ("TurnRight()");

		canMove = false;
		moving = true;
		transform.rotation *= Quaternion.AngleAxis (90f, Vector3.up);	//this rotation is purely for visual representation
		switch(dir){
		case LDIRECTION.FORWARD:
			dir = LDIRECTION.RIGHT;
			break;
		case LDIRECTION.LEFT:
			dir = LDIRECTION.FORWARD;
			break;
		case LDIRECTION.BACKWARD:
			dir = LDIRECTION.LEFT;
			break;
		case LDIRECTION.RIGHT:
			dir = LDIRECTION.BACKWARD;
			break;
		}
	}
}