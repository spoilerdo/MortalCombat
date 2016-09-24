using UnityEngine;
using System.Collections;

public class PositionCamera : MonoBehaviour {

    public float fWidth = 9.0f;  // Desired width 

    void Start()
    {

        float fT = fWidth / Screen.width * Screen.height;
        fT = fT / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        float CameraSize = Camera.main.orthographicSize;
        CameraSize = fT;
        Camera.main.orthographicSize = CameraSize;
    }
}
