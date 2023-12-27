using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;


public class TrailEffect : MonoBehaviour, IGazeFocusable
{
    public GameObject trailPrefab; // 잔상 오브젝트 프리팹
    public float trailInterval = 0.1f; // 잔상 생성 간격 (초)
    public float trailFadeSpeed = 2.0f; // 잔상 투명도 감소 속도

    private float lastTrailTime;
    private Transform trailContainer; // 생성된 잔상을 담을 부모 컨테이너

    [Header("VR and Eyetrack")]
    private bool _hasFocus = false;

    void Start()
    {
        // 부모 컨테이너 생성
        trailContainer = new GameObject("TrailContainer").transform;
    }

    void Update()
    {
        // Get the magnitude of the velocity
        float velocityMagnitude = GetComponent<Rigidbody>().velocity.magnitude;

        // Calculate a new trail interval based on velocity
        float adjustedTrailInterval = Mathf.Lerp(0.5f, 0.05f, velocityMagnitude / 10.0f);

        // 물체가 움직일 때
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
        {
            if (!_hasFocus) return;

            // 일정 간격으로 잔상 생성
            if (Time.time - lastTrailTime > adjustedTrailInterval)
            {
                CreateTrail();
                lastTrailTime = Time.time;
            }
        }
    }

    void CreateTrail()
    {
        // 잔상 오브젝트 생성
        GameObject trailObject = Instantiate(trailPrefab, transform.position, transform.rotation, trailContainer);

        // 투명도 감소 코루틴 시작
        StartCoroutine(FadeTrail(trailObject.GetComponent<Renderer>()));
    }

    IEnumerator FadeTrail(Renderer trailRenderer)
    {
        // 초기 투명도
        Color startColor = trailRenderer.material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;
        float fadeDuration = trailFadeSpeed / 4.0f;

        while (elapsedTime < fadeDuration)
        {
            // 투명도 감소
            trailRenderer.material.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 투명도가 완전히 사라지면 오브젝트 제거
        Destroy(trailRenderer.gameObject);
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
}
