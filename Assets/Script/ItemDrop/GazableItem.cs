using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class GazableItem : MonoBehaviour, IGazeFocusable
{
    private bool _hasFocus = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_hasFocus)
            HapticManager.instance.CallHapticPulse();
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
}
