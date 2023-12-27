using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    public Material originalMaterial;
    public Material newMaterial;

    void Update()
    {
        // ¾îµÎ¿öÁö¸é filpper°¡ ¹à¾ÆÁü
        if (Fever_Penalty.isDark == true)
        {
            ChangeObjectMaterial();
        }
        else
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = originalMaterial;
        }
    }

    void ChangeObjectMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && newMaterial != null)
        {
            renderer.material = newMaterial;
        }
    }
}
