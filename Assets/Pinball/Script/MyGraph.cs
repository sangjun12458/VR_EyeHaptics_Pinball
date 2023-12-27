using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MyGraph : MonoBehaviour
{
    [Range(0, 1)]
    public float duration = 0.1f;
    [Range(0, 320)]
    public float frequency = 1.0f;
    [Range(0, 1)]
    public float amplitude = 1.0f;
    [Range(0, 1)]
    public float interval = 1.0f;
    public float logScale = 2.0f;
    public float exponentialFactor = 1f; // 지수함수에 곱해지는 계수
    public bool keepHaptic = false;

    [SerializeField] private SteamVR_Input_Sources handType1;
    [SerializeField] private SteamVR_Input_Sources handType2;
    [SerializeField] private SteamVR_Action_Vibration hapticAction;
    [SerializeField] private SteamVR_Action_Boolean triggerInput;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HapticPulse());
    }

    // Update is called once per frame
    void Update()
    {
        // 시간에 따른 Sin 함수와 Log 함수의 조합으로 진동 생성
        float time = Time.time;
        float sinValue = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time);
        float logValue = Mathf.Log(time + 1) * logScale;
        float expValue = Mathf.Exp(-exponentialFactor * time);

        float vibrationValue = sinValue * expValue;

        transform.position = new Vector3(transform.position.x, transform.position.y, vibrationValue * 10);

        //frequency = 160 + 160 * Mathf.Sin(time);
        amplitude = 0.5f + 0.5f * Mathf.Sin(time);
    }

    //duration: 0~, frequency: 0~320, amplitude: 0~1
    IEnumerator HapticPulse()
    {
        while (true)
        {
            // 진동 주기에 따라 진동
            hapticAction.Execute(0, duration, frequency, amplitude, handType2);

            // 일정 시간 동안 기다림
            yield return new WaitForSecondsRealtime(interval);
        }
    }
}
