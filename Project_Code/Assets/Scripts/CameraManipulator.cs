using UnityEngine;

public enum CAMERA_POSITION {BOTTOMLEFT, BOTTOMRIGHT, TOPRIGHT, TOPLEFT};

public class CameraManipulator : MonoBehaviour {

    public CAMERA_POSITION camPosition = CAMERA_POSITION.BOTTOMLEFT;

    private void Start() { SwitchCameraTransform(camPosition); }

    public void ButtonPress() {
        switch (camPosition) {
            case CAMERA_POSITION.BOTTOMLEFT:
                camPosition = CAMERA_POSITION.BOTTOMRIGHT;
                SwitchCameraTransform(CAMERA_POSITION.BOTTOMRIGHT);
                break;
            case CAMERA_POSITION.BOTTOMRIGHT:
                camPosition = CAMERA_POSITION.TOPRIGHT;
                SwitchCameraTransform(CAMERA_POSITION.TOPRIGHT);
                break;
            case CAMERA_POSITION.TOPRIGHT:
                camPosition = CAMERA_POSITION.TOPLEFT;
                SwitchCameraTransform(CAMERA_POSITION.TOPLEFT);
                break;
            case CAMERA_POSITION.TOPLEFT:
                camPosition = CAMERA_POSITION.BOTTOMLEFT;
                SwitchCameraTransform(CAMERA_POSITION.BOTTOMLEFT);
                break;
        }
    }

    void SwitchCameraTransform(CAMERA_POSITION TargetTransform)
    {
        switch (camPosition)
        {
            case CAMERA_POSITION.BOTTOMLEFT:
                transform.position = new Vector3(-45, 63.3f, -45);
                transform.rotation = Quaternion.Euler(45, 45, 0);
                break;
            case CAMERA_POSITION.BOTTOMRIGHT:
                transform.position = new Vector3(45, 63.3f, -45);
                transform.rotation = Quaternion.Euler(45, -45, 0);
                break;
            case CAMERA_POSITION.TOPRIGHT:
                transform.position = new Vector3(45, 63.3f, 45);
                transform.rotation = Quaternion.Euler(45, -135, 0);
                break;
            case CAMERA_POSITION.TOPLEFT:
                transform.position = new Vector3(-45, 63.3f, 45);
                transform.rotation = Quaternion.Euler(45, 135, 0);
                break;
        }
    }
}