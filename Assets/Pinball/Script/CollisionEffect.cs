using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Valve.VR;

public class CollisionEffect : MonoBehaviour, IGazeFocusable
{
    public GameObject collisionEffectPrefab; // 충돌 이펙트 프리팹
    public GameObject WeakcollisionEffectPrefab;
    public GameObject PumpEffectPrefab;

    public Material ChargingEffectMaterial;

    public float effectDuration = 1.0f; // 이펙트 지속 시간 (초)
    public float minCollisionSpeed = 5.0f; // 최소 충돌 속도

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public Transform bar;

    [Header("VR and Eye-Track")]
    public static bool _hasFocus = false;
    [SerializeField] private SteamVR_Input_Sources handType1;
    [SerializeField] private SteamVR_Input_Sources handType2;
    [SerializeField] private SteamVR_Action_Vibration hapticAction;


    private bool _isHaptics = false;
    private bool _isWall = true;
    private float elapsedTime = 0f;
    private float duration = 0.1f;
    private float frequency = 0f;
    private float amplitude = 0f;
    private float interval = 0.1f;

    void Update()
    {
        if (!_isHaptics)
            return;

        if (_isWall)
        {
            amplitude = Mathf.Sin(Time.time) * Mathf.Exp(1f - elapsedTime);
            amplitude = (amplitude / Mathf.Exp(1f) + 1f) / 2f;
            frequency = Mathf.Sin(Time.time) * Mathf.Exp(1f - elapsedTime);
            frequency = (frequency / Mathf.Exp(1f)) * 160f + 160f;
        }
        else
        {
            amplitude = Mathf.Sin(Time.time) * Mathf.Log(2f - elapsedTime);
            amplitude = (amplitude / Mathf.Log(2f)) / 2f + 0.5f; 
            frequency = Mathf.Sin(Time.time) * Mathf.Log(2f - elapsedTime);
            frequency = (frequency / Mathf.Log(2f)) * 160f + 160f;
        }
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            elapsedTime = 0f;
            _isHaptics = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 오브젝트의 현재 속도를 확인
        float collisionSpeed = collision.relativeVelocity.magnitude;

        if (Fever_Penalty.isFever == true || Fever_Penalty.isDark == true)
            audioSource.spatialBlend = 1f;
        else
            audioSource.spatialBlend = Mathf.Clamp01(bar.localScale.x);
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        if (_hasFocus)
            audioSource.volume = Mathf.Clamp(collisionSpeed / 60f, 0f, 0.5f);
        else
            audioSource.volume = 0.4f;
        audioSource.Play();

        _isWall = collision.gameObject.tag == "Wall" ? true : false;
        _isHaptics = true;
        string[] names = { "Plunger", "PlungerTrigger", "PlungerBoard" };

        if (collision.gameObject.name != "Plunger" && collision.gameObject.name != "EffectWall_4" && collision.gameObject.name != "EffectWall_5")
        {
            if (collision.gameObject.tag == "Pump" || collision.gameObject.tag == "Wall")
            {
                if (_hasFocus)
                    StartCoroutine(HapticPulse());
            }
        }

        if (Fever_Penalty.isDark == false) //밝을때만 이펙트 생성
        {
            if (collisionSpeed >= minCollisionSpeed) //최소 충돌 속도를 넘을 때만 이펙트 생성
            {
                string collidedObjectTag = collision.gameObject.tag;

                if (collidedObjectTag != "Floor" && _hasFocus)
                {

                    if (collidedObjectTag == "Pump")
                    {
                        // 충돌 지점을 얻어옴
                        ContactPoint contact = collision.contacts[0];
                        Vector3 collisionPoint = contact.point;

                        // 충돌 이펙트 생성
                        CreatePumpEffect(collisionPoint);
                    }

                    if (collidedObjectTag == "Wall")
                    {
                        // 충돌 지점을 얻어옴
                        ContactPoint contact = collision.contacts[0];
                        Vector3 collisionPoint = contact.point;

                        // 충돌 이펙트 생성
                        CreateCollisionEffect(collisionPoint);
                    }
                }
            }
            else
            {
                string collidedObjectTag = collision.gameObject.tag;

                if (collidedObjectTag == "Wall")
                {
                    // 충돌 지점을 얻어옴
                    ContactPoint contact = collision.contacts[0];
                    Vector3 collisionPoint = contact.point;

                    // 충돌 이펙트 생성
                    CreateCollisionEffectWeak(collisionPoint);
                }
            }
        }
    }

    void CreateCollisionEffect(Vector3 position) //특정속도 이상으로 부딫혔을 때
    {
        // 충돌 이펙트 오브젝트 생성
        GameObject effectObject = Instantiate(collisionEffectPrefab, position, Quaternion.identity);

        // 이펙트 지속 시간 후에 제거
        Destroy(effectObject, effectDuration);
    }

    void CreateCollisionEffectWeak(Vector3 position) //약하게 부딫혔을 때
    {
        // 충돌 이펙트 오브젝트 생성
        GameObject effectObject = Instantiate(WeakcollisionEffectPrefab, position, Quaternion.identity);

        // 이펙트 지속 시간 후에 제거
        Destroy(effectObject, effectDuration);
    }

    void CreatePumpEffect(Vector3 position) //펌프에 부딫혔을 때 효과
    {
        // 충돌 이펙트 오브젝트 생성
        GameObject effectObject = Instantiate(PumpEffectPrefab, position, Quaternion.identity);

        // 이펙트 지속 시간 후에 제거
        Destroy(effectObject, effectDuration);
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

    IEnumerator HapticPulse()
    {
        while (_isHaptics)
        {
            // 진동 주기에 따라 진동

            hapticAction.Execute(0, duration, frequency, amplitude, handType1);
            hapticAction.Execute(0, duration, frequency, amplitude, handType2);

            // 일정 시간 동안 기다림
            yield return new WaitForSecondsRealtime(interval);
        }
    }
}
