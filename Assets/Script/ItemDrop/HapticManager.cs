using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HapticManager : MonoBehaviour
{
    public static HapticManager instance;

    [SerializeField] private SteamVR_Input_Sources handType;
    [SerializeField] private SteamVR_Action_Vibration hapticAction;

    private float timeThreshold = 1f;
    private float cumulativeTime = 0f;
    private bool hasTimerStarted = false;

    private float d = 0.01f;
    private float f = 0f;
    private float a = 0.5f;
    private float i = 0.01f;

    public bool isExp = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTimerStarted) 
        {
            cumulativeTime += Time.deltaTime;
            if (cumulativeTime > timeThreshold) 
            {
                hasTimerStarted = false;
                cumulativeTime = 0f;
            }

            //float x = cumulativeTime * (-60f / timeThreshold) + 30f; // 30 ~ -30
            //float y = 3f * Mathf.Exp(0.1f * x) * Mathf.Cos(2f * Mathf.PI * x); // 0 ~ 50
            //
            //f = 320f - Mathf.Clamp01(y / 50)*320;
            if (isExp)
            {
                float x1 = cumulativeTime * (8f) - 4f;
                float x2 = cumulativeTime * (-8f) + 4f;
                f = (Mathf.Exp(x1) / 7f) * 320f;
                a = Mathf.Clamp01(Mathf.Exp(x2) / 7f);
            }
            else
            {
                a = Mathf.Sin(Time.time) * Mathf.Log(2f - cumulativeTime);
                a = (a / Mathf.Log(2f)) / 2f + 0.5f;
                f = Mathf.Sin(Time.time) * Mathf.Log(2f - cumulativeTime);
                f = (f / Mathf.Log(2f)) * 160f + 160f;

            }

        }
    }

    public void CallHapticPulse()
    {
        //hapticAction.Execute(0f, 0.2f, 100f, 0.5f, handType);
        hasTimerStarted = true;
        StartCoroutine(HapticPulse());
    }

    IEnumerator HapticPulse()
    {
        while (hasTimerStarted)
        {
            // 진동 주기에 따라 진동

            hapticAction.Execute(0, d, f, a, handType);

            // 일정 시간 동안 기다림
            yield return new WaitForSecondsRealtime(i);
        }
    }
}
