using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.G2OM;
using Valve.VR;

public class PlungerScript : MonoBehaviour, IGazeFocusable
{
    public GameObject EffectPrefab; // 충돌 이펙트 프리팹
    public float effectDuration = 1.0f;
    public float maxPower = 100f;
    public Slider powerSlider;
    public Color startColor = Color.green;
    public Color endColor = Color.red;
    public float colorChangeDuration = 2.0f;
    float power;

    List<Rigidbody> ballList;
    public static bool ballReady;
    private bool isSpacebarPressed;
    private Vector3 sliderPos;

    [Header("VR and Eyetrack")]
    [SerializeField] private SteamVR_Action_Boolean gripAction;
    [SerializeField] private SteamVR_Action_Vibration hapticAction;
    [SerializeField] private SteamVR_Input_Sources rightHand;
    [SerializeField] private Transform controller;
    private Vector3 prevPosition;
    private bool _hasFocus = false;

    [Range(0f, 5f)] public float du = 0.1f;
    [Range(0f, 320f)] public float fr = 80f;
    [Range(0f, 1f)] public float am = 0.25f;
    private float minPeriod = 1f;
    private float maxPeriod = 0.1f;

    //teleportAction.GetState(hand.handType);
    //public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    //public SteamVR_Action_Boolean yourBooleanAction;
    //public SteamVR_Action_Vector2 yourVector2Action;
    //SteamVR_Input_Sources


    void Start()
    {
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
        ballList = new List<Rigidbody>();
        sliderPos = powerSlider.gameObject.transform.position;

        //controller = GameObject.Find("HandColliderRight(Clone)").transform;
        controller = GameObject.Find("RightHand").transform;
        
    }

    void Update()
    {
        UpdateSliderColor();

        powerSlider.value = power;

        if (ballReady)
        {
            powerSlider.gameObject.transform.position = sliderPos;
            powerSlider.gameObject.SetActive(true);

            // Check if the spacebar is pressed
            //isSpacebarPressed = Input.GetKey(KeyCode.Space);
            isSpacebarPressed = gripAction.GetState(rightHand);
            if (gripAction.GetStateDown(rightHand))
            {
                prevPosition = controller.position;
                StartCoroutine(HapticPulse());
            }

            // If spacebar is pressed, apply shaking effect
            if (isSpacebarPressed && _hasFocus)
            {
                ApplyShakingEffect();
            }
        }
        else
        {
            powerSlider.gameObject.SetActive(false);
        }

        if (ballList.Count > 0)
        {
            ballReady = true;

            if (isSpacebarPressed)
            {
                //HapticPulse(du, fr, am);
                if (power <= maxPower)
                {
                    //power += 150 * Time.deltaTime;
                    power = Mathf.Min(500 * (prevPosition.z - controller.position.z), maxPower);

                }
            }

            //if (Input.GetKeyUp(KeyCode.Space))
            if (gripAction.GetStateUp(rightHand))
            {
                //HapticPulse(0f, 0f, 0f);
                foreach (Rigidbody r in ballList)
                {
                    r.AddForce(power * Vector3.forward);
                }

                if (Fever_Penalty.isDark != true)
                {
                    CreateEffect();
                }
            }
        }
        else
        {
            ballReady = false;
            power = 0f;
            powerSlider.value = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Remove(other.gameObject.GetComponent<Rigidbody>());
            power = 0f;
            ballReady = false;
            powerSlider.value = 0;
        }
    }

    void UpdateSliderColor()
    {
        // Change the color of the slider gradually based on the power level
        float normalizedPower = Mathf.Clamp01(power / maxPower);
        powerSlider.fillRect.GetComponent<Image>().color = Color.Lerp(startColor, endColor, normalizedPower);
    }

    void ApplyShakingEffect()
    {
        // Calculate normalized distance to max power
        float normalizedDistance = Mathf.Clamp01(power / maxPower);

        // Adjust the shaking frequency and intensity based on the proximity to max power
        float shakingFrequency = Mathf.Lerp(5f, 20f, normalizedDistance); // 흔들리는 주기
        float shakingIntensity = Mathf.Lerp(0.01f, 0.1f, normalizedDistance); // 흔들리는 범위

        // Shaking effect on the slider (horizontal movement)
        float shaking = Mathf.Sin(Time.time * shakingFrequency) * shakingIntensity;
        powerSlider.transform.position = new Vector3(powerSlider.transform.position.x + shaking, powerSlider.transform.position.y, powerSlider.transform.position.z);
    }


    void CreateEffect()
    {
        Vector3 position = gameObject.transform.position;
        float effectSize = _hasFocus ? 1f : 0.3f;
        EffectPrefab.transform.localScale = new Vector3(effectSize, effectSize, effectSize);
        // 충돌 이펙트 오브젝트 생성
        GameObject effectObject = Instantiate(EffectPrefab, position, Quaternion.identity);

        // 이펙트 지속 시간 후에 제거
        Destroy(effectObject, effectDuration);
        power = 0f;
        ballReady = false;
        powerSlider.value = 0;
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

    //duration: 0~, frequency: 0~320, amplitude: 0~1
    IEnumerator HapticPulse()
    {
        while (isSpacebarPressed)
        {
            // 진동 주기에 따라 진동
            hapticAction.Execute(0, du, fr, am, rightHand);

            float normalizedPower = Mathf.Clamp01(power / maxPower);
            float vibrationPeriod = _hasFocus ? Mathf.Lerp(minPeriod, maxPeriod, normalizedPower) : (minPeriod + maxPeriod) / 2;

            // 일정 시간 동안 기다림
            yield return new WaitForSecondsRealtime(vibrationPeriod);
        }
    }
}
