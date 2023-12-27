using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneButton.sceneName != "Pinball")
        {
            audioSource.Stop();
            return;
        }

        audioSource.clip = audioClips[Fever_Penalty.isFever ? 1 : 0];
        if (!audioSource.isPlaying)
            audioSource.Play();

        if (Fever_Penalty.isDark)
        {
            audioMixer.SetFloat("BGM", -20);
            audioMixer.SetFloat("SFX", 5);
        }
        else
        {
            audioMixer.SetFloat("BGM", 0);
            audioMixer.SetFloat("SFX", 0);
        }
    }
}
