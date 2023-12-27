using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShake : MonoBehaviour
{
    public float shakeIntensity = 0.1f; // 흔들림 강도
    public float shakeDuration = 0.2f; // 지속시간
    public float minCollisionSpeed = 5.0f; // 일정 속도 이상일 때만 흔들림 발생

    private Vector3 originalPosition;

    public Material originalMaterial;
    public Material GlowingEffectMaterial;

    void Start()
    {
        // 물체의 원래 위치 저장
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        //현재 물체를 바라보고 있는지 확인하는 코드입력하기

        // 상대 속도 측정
        float collisionSpeed = collision.relativeVelocity.magnitude;

        // 속도확인
        if (collisionSpeed > minCollisionSpeed && collision.gameObject.CompareTag("Ball") && CollisionEffect._hasFocus)
        {
            // 물체 흔들기
            StartCoroutine(Shake());

            // 컨트롤러에게 진동 주기
        }

        //if (Fever_Penalty.isDark == true)
        if (collision.gameObject.tag == "Ball") 
            StartCoroutine(Glow());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * shakeIntensity;
            transform.position = originalPosition + shakeOffset;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 흔들림 이후 위치 초기화
        transform.position = originalPosition;
    }

    IEnumerator Glow()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && GlowingEffectMaterial != null) //패널티 모드에서 공의 위치를 순간 순간 알려주기 위한 이펙트(반짝임)
        {
            renderer.material = GlowingEffectMaterial;
            yield return new WaitForSeconds(0.2f); //반짝임 지속시간
        }

        if (renderer != null && originalMaterial != null) //원래의 메테리얼 적용
        {
            renderer.material = originalMaterial;
        }
    }
}