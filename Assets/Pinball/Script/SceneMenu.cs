using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SceneMenu : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean triggerInput;
    [SerializeField] private SteamVR_Input_Sources handType;
    private bool _isActiveUI = false;
    public GameObject SceneUI;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerInput.GetStateDown(handType))
            _isActiveUI = !_isActiveUI;

        SceneUI.SetActive(_isActiveUI);

        transform.position = target.position + new Vector3(0f, -1f, 12f);
    }
}
