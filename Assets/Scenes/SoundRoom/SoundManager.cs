using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public bool isOn = false;
    [Range(20f, -80f)] public float volume = -15f;
    private float v = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int cnt = 0;
        for (int i = 0; i < 7; i++)
        {
            if (EyeTrackingSound.focusArr[i])
                cnt++;
        }
        if (cnt == 0)
        {
            v = Mathf.Min(v + Time.deltaTime, 20);
        }
        else
        {
            v -= Time.deltaTime;
            if (v < volume) v = volume;
        }
        audioMixer.SetFloat("SFX", v);
        audioMixer.SetFloat("BGM", v);

    }
}
