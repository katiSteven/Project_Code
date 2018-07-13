using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderButton : MonoBehaviour {

	public float min, max, mid,animationTime;


	bool buttonPressed = false;
	float yMov;

	// Use this for initialization
	void Start () {
		yMov = min;
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
	}

	public void OnPointerUp(){
		buttonPressed = false;
		if (yMov > mid) {
			yMov = max;
		} else {
			yMov = min;
		}
	}
}
