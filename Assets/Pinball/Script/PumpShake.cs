using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpShake : MonoBehaviour
{
    public float scaleChange = 0.5f; // 스케일 변경 값
    public float scaleDuration = 0.4f; // 스케일 변경 지속시간

    private Vector3 originalScale;

    public Material originalMaterial;
    public Material GlowingEffectMaterial;

    void Start()
    {
        // 물체의 원래 스케일 저장
        originalScale = transform.localScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 상대 속도 측정
        float collisionSpeed = collision.relativeVelocity.magnitude;

        // 속도 확인
        if (collision.gameObject.CompareTag("Ball") && CollisionEffect._hasFocus)
        {
            // 효과 주기
            StartCoroutine(ScaleEffect());
        }

        //if(Fever_Penalty.isDark == true)
        //{
            StartCoroutine(Glow());
        //}
    }

    IEnumerator ScaleEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            float t = elapsedTime / scaleDuration;

            // 스케일 변경
            float newScale = Mathf.Lerp(1f, 1f - scaleChange, t);
            transform.localScale = originalScale * newScale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 원래 크기로 복구
        transform.localScale = originalScale;
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
