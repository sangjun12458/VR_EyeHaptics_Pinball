using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Valve.VR;

public class FlipperScript : MonoBehaviour, IGazeFocusable
{
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrenght = 10000f;
    public float flipperDamper = 150f;
    HingeJoint hinge;

    //Name of Axis
    public string inputName;

    [Header("VR and Eye-Track")]
    [SerializeField] private SteamVR_Action_Boolean triggerInput;
    [SerializeField] private SteamVR_Input_Sources handType;
    [SerializeField] private SteamVR_Action_Vibration hapticAction;
    private bool _hasFocus = false;
    private Vector3 prevPosition;
    [SerializeField] private Transform controller;
    [SerializeField] private float moveDistance = 0.3f;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;

        if (handType == SteamVR_Input_Sources.RightHand)
            controller = GameObject.Find("RightHand").transform;
        else if (handType == SteamVR_Input_Sources.LeftHand)
            controller = GameObject.Find("LeftHand").transform;
    }


    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrenght;
        spring.damper = flipperDamper;

/*        if (triggerInput.GetStateDown(handType))
        {
            prevPosition = controller.position;
            if (controller.position.y - prevPosition.y > moveDistance)
            {
                spring.targetPosition = pressedPosition;
            }
            else if (prevPosition.y - controller.position.y < 0.001f)
            {
                spring.targetPosition = restPosition;
            }
        }*/
        if (triggerInput.GetState(handType)) 
        {
            spring.targetPosition = pressedPosition;
        }
        else
        {
            spring.targetPosition = restPosition;
        }


        hinge.spring = spring;
        hinge.useLimits = true;
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        _hasFocus = hasFocus;
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
        }
    }

    //duration: 0~, frequency: 0~320, amplitude: 0~1
    void HapticPulse(float dur, float fre, float amp)
    {
        hapticAction.Execute(0, dur, fre, amp, handType);
       
    }
}
