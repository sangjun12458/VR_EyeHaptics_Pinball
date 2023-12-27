using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverColorScript : MonoBehaviour
{
    public Material original;
    public Material fever;
    private Renderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = original;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fever_Penalty.isFever == true)
            renderer.material = fever;
        else
            renderer.material = original;
    }
}
