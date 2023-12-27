using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.XR;


public class TempEyeInfo : MonoBehaviour
{
    public Text text;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        var rayDirection = eyeTrackingData.GazeRay.Direction;
        
        text.text = eyeTrackingData.ConvergenceDistance.ToString();
    }
}
