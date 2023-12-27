using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{

    void Update()
    {
        if(Fever_Penalty.isDark == true)
        {
            Light light = GetComponent<Light>();
            light.intensity = 0;
        }
        else
        {
            Light light = GetComponent<Light>();
            light.intensity = 0.6f;
        }
    }
}
