using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Valve.VR;

public class CollisionEffect : MonoBehaviour, IGazeFocusable
{
    public GameObject collisionEffectPrefab; // �浹 ����Ʈ ������
    public GameObject WeakcollisionEffectPrefab;
    public GameObject PumpEffectPrefab;

    public Material ChargingEffectMaterial;

    public float effectDuration = 1.0f; // ����Ʈ ���� �ð� (��)
    public float minCollisionSpeed = 5.0f; // �ּ� �浹 �ӵ�

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
        // ������Ʈ�� ���� �ӵ��� Ȯ��
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

        if (Fever_Penalty.isDark == false) //�������� ����Ʈ ����
        {
            if (collisionSpeed >= minCollisionSpeed) //�ּ� �浹 �ӵ��� ���� ���� ����Ʈ ����
            {
                string collidedObjectTag = collision.gameObject.tag;

                if (collidedObjectTag != "Floor" && _hasFocus)
                {

                    if (collidedObjectTag == "Pump")
                    {
                        // �浹 ������ ����
                        ContactPoint contact = collision.contacts[0];
                        Vector3 collisionPoint = contact.point;

                        // �浹 ����Ʈ ����
                        CreatePumpEffect(collisionPoint);
                    }

                    if (collidedObjectTag == "Wall")
                    {
                        // �浹 ������ ����
                        ContactPoint contact = collision.contacts[0];
                        Vector3 collisionPoint = contact.point;

                        // �浹 ����Ʈ ����
                        CreateCollisionEffect(collisionPoint);
                    }
                }
            }
            else
            {
                string collidedObjectTag = collision.gameObject.tag;

                if (collidedObjectTag == "Wall")
                {
                    // �浹 ������ ����
                    ContactPoint contact = collision.contacts[0];
                    Vector3 collisionPoint = contact.point;

                    // �浹 ����Ʈ ����
                    CreateCollisionEffectWeak(collisionPoint);
                }
            }
        }
    }

    void CreateCollisionEffect(Vector3 position) //Ư���ӵ� �̻����� �΋H���� ��
    {
        // �浹 ����Ʈ ������Ʈ ����
        GameObject effectObject = Instantiate(collisionEffectPrefab, position, Quaternion.identity);

        // ����Ʈ ���� �ð� �Ŀ� ����
        Destroy(effectObject, effectDuration);
    }

    void CreateCollisionEffectWeak(Vector3 position) //���ϰ� �΋H���� ��
    {
        // �浹 ����Ʈ ������Ʈ ����
        GameObject effectObject = Instantiate(WeakcollisionEffectPrefab, position, Quaternion.identity);

        // ����Ʈ ���� �ð� �Ŀ� ����
        Destroy(effectObject, effectDuration);
    }

    void CreatePumpEffect(Vector3 position) //������ �΋H���� �� ȿ��
    {
        // �浹 ����Ʈ ������Ʈ ����
        GameObject effectObject = Instantiate(PumpEffectPrefab, position, Quaternion.identity);

        // ����Ʈ ���� �ð� �Ŀ� ����
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
            // ���� �ֱ⿡ ���� ����

            hapticAction.Execute(0, duration, frequency, amplitude, handType1);
            hapticAction.Execute(0, duration, frequency, amplitude, handType2);

            // ���� �ð� ���� ��ٸ�
            yield return new WaitForSecondsRealtime(interval);
        }
    }
}
