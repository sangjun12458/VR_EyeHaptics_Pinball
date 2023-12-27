using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using UnityEngine.Audio;

public class EyeTrackingSound : MonoBehaviour, IGazeFocusable
{
    private enum Mode
    {
        None,
        Fixation,
        SmoothPursuit,
        Saccade,
        ExceptFixation,
        ExceptSmoothPursuit,
        ExceptSaccde
    }

    [SerializeField]
    private Mode mode = Mode.None;

    private AudioSource sound;


    private float fixationThreshold = 1.0f;
    private float fixationTime = 0.0f;
    private float smoothPursuitThreshold = 1.0f;
    private float smoothPursuitTime = 0.0f;

    private bool _hasFocus = false;

    public static bool[] focusArr = new bool[7];
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

        switch (mode)
        {
            case Mode.Fixation:
            case Mode.SmoothPursuit:
                sound.Play(); 
                break;
            case Mode.Saccade:
                sound.Stop();
                break;
            default: 
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.Fixation:
                if (!_hasFocus)
                {
                    fixationTime = 0.0f;
                    sound.volume = Mathf.Max(sound.volume - Time.deltaTime, 0.1f);
                    focusArr[num] = false;
                }
                else
                {
                    focusArr[num] = true;
                    fixationTime += Time.deltaTime;
                    if (fixationTime > fixationThreshold)
                    {
                        sound.volume = Mathf.Min(sound.volume + Time.deltaTime, 1f);
                    }
                }
                break;
            case Mode.SmoothPursuit:

                break;

            case Mode.Saccade:
                if (!_hasFocus)
                    sound.Stop();
                else
                    if (!sound.isPlaying)
                        sound.Play();
                break;

            case Mode.ExceptFixation:
                break;

            case Mode.ExceptSmoothPursuit: 
                break;

            case Mode.ExceptSaccde: 
                break;

            default:
                break;
            
        }
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

/*    void OnMouseEnter()
    {
        isGazed = true;
    }

    void OnMouseOver()
    {

    }

    void OnMouseExit() 
    {
        isGazed = false;
    }*/
}
