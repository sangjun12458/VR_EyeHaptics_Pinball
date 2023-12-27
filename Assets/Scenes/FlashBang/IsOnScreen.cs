using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;

public class IsOnScreen : MonoBehaviour
{
    private GameObject playerCamObject;
    private Camera mainCamera;
    public Transform cubeTransform;
    public static bool isSeen = false;

    private void Awake()
    {
        playerCamObject = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera = playerCamObject.GetComponent<Camera>();
    }

    void Update()
    {
/*        if (mainCamera == null || cubeTransform == null)
            return;

        // Convert cube's position to viewport coordinates
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(cubeTransform.position);

        // Check if the cube is within the camera's view
        bool isVisible = (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0);

        if (isVisible)
        {
            // Cube is within the camera's view
            isSeen = true;
        }
        else
        {
            // Cube is not within the camera's view
            isSeen = false;
        }*/

        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        if (eyeTrackingData.IsLeftEyeBlinking && eyeTrackingData.IsRightEyeBlinking)
            isSeen = false;
        else
            isSeen = true;
    }
}
