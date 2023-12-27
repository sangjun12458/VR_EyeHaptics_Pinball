using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWhenFever : MonoBehaviour
{
    public Material originalMaterial;
    public Material GlowingEffectMaterial;


    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (Fever_Penalty.isFever == true)
        {
            if (GetComponent<Renderer>() != null && GlowingEffectMaterial != null) 
            {
                GetComponent<Renderer>().material = GlowingEffectMaterial;
            
            }
        }
        else
        {
            if (GetComponent<Renderer>() != null && originalMaterial != null) 
            {
                GetComponent<Renderer>().material = originalMaterial;
            }
        }
        
    }
}
