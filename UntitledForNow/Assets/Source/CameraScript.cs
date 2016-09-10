using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    GameObject FocusTarget; // the camera orbits around this invisible object

    #region camera motion parameters

    Vector2 MouseOld = new Vector2(0, 0); // for tracking mouse displacement
    Vector3 TempVector3 = new Vector3(0, 0, 0); // use this for math but don't realloc memory

    #warning replace this with some config file

    Vector2 CameraRotationRate = new Vector2(0.3f, 0.3f); // (horizontal, vertical)
    Vector2 CameraRotation = new Vector2(0, 0); // (X, Y), not a Quaternion
    float CameraUpperLimit = 80; // maximum upwards tilt
    float CameraLowerLimit = 10; // lowest camera tilt
    float CameraZoomRate = 5; // variable depends on a user's mouse wheel speed
    float CameraDistanceMin = 4; // lowest zoom-in distance
    float CameraDistanceMax = 30; // furthest zoom-out distance
    float CameraDistance = 15;
    bool IsCameraLocked = true; // if the FocusTarget is locked on to another object
    float CameraPanRate = 1; // similar use as the CameraZoomRate

    #endregion

    // Use this for initialization
    void Start () {
        //Application.targetFrameRate = 60;
        Camera mainCam = Camera.allCameras[0];
        mainCam.aspect = 1.6f;

        FocusTarget = transform.parent.gameObject;

        // init the mouse position so it doesn't jump at frame 1
        MouseOld.x = Input.mousePosition.x;
        MouseOld.y = Input.mousePosition.y;

        //Debug.Log("test : " + FocusTarget.name);
        FocusTarget.transform.parent = GameObject.Find("fillerCar").transform;
        Debug.Log("test : " + FocusTarget.transform.parent.name);
        TempVector3.Set(0, 0, 0);
        FocusTarget.transform.localPosition = TempVector3;
    }
	
	// Update is called once per frame
	void Update () {
	    // do nothing
	}

    // LateUpdate occurs after Update so this ensures the 3rd person camera
    // can track the motion from the prior Update
    void LateUpdate()
    {
        #region conditional calculations

        if (Input.GetMouseButton(1)) // hold right button to rotate
        {
            CameraRotation.x += (Input.mousePosition.x - MouseOld.x) * CameraRotationRate.x;
            CameraRotation.y -= (Input.mousePosition.y - MouseOld.y) * CameraRotationRate.y;
        }

        if (!IsCameraLocked)
        {
            #warning add camera panning code here
        }

        #endregion

        #region camera motions

        MouseOld.x = Input.mousePosition.x; // update the mouse position tracking
        MouseOld.y = Input.mousePosition.y;

        // clamp and set bounds to rotation values
        CameraRotation.x = CameraRotation.x % 360;
        if (CameraRotation.y > CameraUpperLimit)
        {
            CameraRotation.y = CameraUpperLimit;
        }
        else if (CameraRotation.y < CameraLowerLimit)
        {
            CameraRotation.y = CameraLowerLimit;
        }

        // apply zoom and boundaries
        CameraDistance -= CameraDistance * Input.GetAxis("Mouse ScrollWheel"); // proportional zoom

        if (CameraDistance > CameraDistanceMax)
        {
            CameraDistance = CameraDistanceMax;
        }
        else if (CameraDistance < CameraDistanceMin)
        {
            CameraDistance = CameraDistanceMin;
        }

        // package / format rotation as a Quaternion
        Quaternion rotation = Quaternion.Euler(CameraRotation.y, CameraRotation.x, 0);

        // transform is the camera
        TempVector3.Set(0, 0, -CameraDistance);
        transform.position = rotation * TempVector3 + FocusTarget.transform.position;
        transform.rotation = rotation;

        #endregion
    }
}
