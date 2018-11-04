using UnityEngine;

public class SliderButton : MonoBehaviour {

	public float min, max, movementThreshold, animationTime;
	public bool openOnLoad;

	float dragStart, dragEnd;
	bool buttonPressed = false;
	float yMov;

	// Use this for initialization
	void Start () {
		if (openOnLoad) {
			yMov = max;
		} else {
			yMov = min;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonPressed) {
			yMov = Input.mousePosition.y;
			transform.position = new Vector3 (transform.position.x, Mathf.Clamp (yMov, min, max), 0);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x,yMov, 0), Time.deltaTime * animationTime);
		}
	}

	public void OnPointerDown(){
		buttonPressed = true;
		dragStart = Input.mousePosition.y;
	}

	public void OnPointerUp(){
		buttonPressed = false;
		dragEnd = Input.mousePosition.y;

		float dragDistance = dragStart - dragEnd;
		print (dragDistance);

		if (dragDistance < 0) {
			if (dragDistance <= -movementThreshold) {
				yMov = max;
			} else {
				yMov = min;
			}
		}

		if(dragDistance > 0){
			if (dragDistance >= movementThreshold) {
				yMov = min;
			} else {
				yMov = max;
			}
		}
	}
}
