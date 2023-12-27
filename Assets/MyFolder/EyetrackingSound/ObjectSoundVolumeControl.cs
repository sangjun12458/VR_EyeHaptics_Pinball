using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundVolumeControl : MonoBehaviour
{
    private AudioSource objectAudioSource;
    public Camera mainCamera; 

    public float maxDistance = 250.0f; 
    public float minDistance = 50.0f; 
    public float maxVolume = 1.0f; 
    public float minVolume = 0.1f;


    [SerializeField] 
    private bool isPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        objectAudioSource = GetComponent<AudioSource>();

        if (objectAudioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned to the object.");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying) 
        {
            Vector3 objectPosition = transform.position;

            Vector3 objectScreenPosition = mainCamera.WorldToScreenPoint(objectPosition);
            objectScreenPosition.z = 0;

            Vector3 mouseScreenPosition = Input.mousePosition;

            float distanceToMouse = Vector3.Distance(objectScreenPosition, mouseScreenPosition);

            float normalizedDistance = 1.0f - Mathf.Clamp01((distanceToMouse - minDistance) / (maxDistance - minDistance));
            float volume = Mathf.Lerp(minVolume, maxVolume, normalizedDistance);

            if (objectAudioSource != null)
            {
                objectAudioSource.volume = volume;
            }

            //DebugSoundMessage(objectScreenPosition, mouseScreenPosition, distanceToMouse);
        }
        else
        {
            Vector3 objectPosition = transform.position;

            Vector3 objectScreenPosition = mainCamera.WorldToScreenPoint(objectPosition);
            objectScreenPosition.z = 0;

            Vector3 mouseScreenPosition = Input.mousePosition;

            float distanceToMouse = Vector3.Distance(objectScreenPosition, mouseScreenPosition);

            if (distanceToMouse < minDistance)
                objectAudioSource.volume = maxVolume;
            else
                objectAudioSource.volume = 0;
        }
    }


    void DebugSoundMessage(Vector3 pos1, Vector3 pos2, float dis)
    {
        string text = "";
        text += pos1.ToString();
        text += "   ";
        text += pos2.ToString();
        text += "   ";
        text += dis;
        Debug.Log(text);
    }
}

