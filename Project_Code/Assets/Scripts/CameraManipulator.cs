using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CAMERA_POSITION {BOTTOMLEFT, BOTTOMRIGHT, TOPRIGHT, TOPLEFT};

public class CameraManipulator : MonoBehaviour {

    public CAMERA_POSITION camPosition = CAMERA_POSITION.BOTTOMLEFT;

    public void ButtonPress() {
        switch (camPosition) {
            case CAMERA_POSITION.BOTTOMLEFT:
                camPosition = CAMERA_POSITION.BOTTOMRIGHT;
                transform.position = new Vector3(45, 63.3f, -45);
                transform.rotation = Quaternion.Euler(45, -45, 0);
                //transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
                //transform.rotation *= Quaternion.AngleAxis(-45, Vector3.up);
                //transform.rotation *= Quaternion.AngleAxis(-90, Vector3.up);
                break;
            case CAMERA_POSITION.BOTTOMRIGHT:
                camPosition = CAMERA_POSITION.TOPRIGHT;
                transform.position = new Vector3(45, 63.3f, 45);
                transform.rotation = Quaternion.Euler(45, -135, 0);
                //transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
                //transform.rotation *= Quaternion.AngleAxis(-135, Vector3.up);
                //transform.rotation *= Quaternion.AngleAxis(-45, Vector3.up);
                break;
            case CAMERA_POSITION.TOPRIGHT:
                camPosition = CAMERA_POSITION.TOPLEFT;
                transform.position = new Vector3(-45, 63.3f, 45);
                transform.rotation = Quaternion.Euler(45, 135, 0);
                //transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
                //transform.rotation *= Quaternion.AngleAxis(135, Vector3.up);
                //transform.rotation *= Quaternion.AngleAxis(270, Vector3.up);
                break;
            case CAMERA_POSITION.TOPLEFT:
                camPosition = CAMERA_POSITION.BOTTOMLEFT;
                transform.position = new Vector3(-45, 63.3f, -45);
                transform.rotation = Quaternion.Euler(45, 45, 0);
                //transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
                //transform.rotation *= Quaternion.AngleAxis(45, Vector3.up);
                //transform.rotation *= Quaternion.AngleAxis(-90, Vector3.up);
                break;
        }
    }

}
