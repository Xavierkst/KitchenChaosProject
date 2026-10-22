using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    // obtain the transform for this progressBarUI and assign it the Camera's position
    // and then flip that vector
    private enum Mode { 
        LookAt, 
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    };

    [SerializeField] private Mode mode;

    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                // Target pos passed in is the camera's position:
                this.transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                // The target is opposite from the camera:
                this.transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                this.transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                this.transform.forward = -Camera.main.transform.forward;
                break;
        }
    }

}
