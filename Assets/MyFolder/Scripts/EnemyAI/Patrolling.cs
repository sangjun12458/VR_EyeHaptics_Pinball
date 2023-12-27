using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [Header("Eye Interaction Info")]
    [SerializeField]
    private bool eyeInteractionEnabled = false;
    [SerializeField]
    private bool isGazeContact = false;
    [SerializeField]
    private float gazeContactTime = 0.0f;
    [Range(0.0f, 2.0f)]
    public float gazeTriggerTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        isGazeContact = true;
    }

    private void OnMouseExit()
    {
        gazeContactTime = 0.0f;
        isGazeContact = false;
    }
}
