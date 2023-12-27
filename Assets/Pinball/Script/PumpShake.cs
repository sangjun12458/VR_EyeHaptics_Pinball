using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpShake : MonoBehaviour
{
    public float scaleChange = 0.5f; // ������ ���� ��
    public float scaleDuration = 0.4f; // ������ ���� ���ӽð�

    private Vector3 originalScale;

    public Material originalMaterial;
    public Material GlowingEffectMaterial;

    void Start()
    {
        // ��ü�� ���� ������ ����
        originalScale = transform.localScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        // ��� �ӵ� ����
        float collisionSpeed = collision.relativeVelocity.magnitude;

        // �ӵ� Ȯ��
        if (collision.gameObject.CompareTag("Ball") && CollisionEffect._hasFocus)
        {
            // ȿ�� �ֱ�
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

            // ������ ����
            float newScale = Mathf.Lerp(1f, 1f - scaleChange, t);
            transform.localScale = originalScale * newScale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ũ��� ����
        transform.localScale = originalScale;
    }

    IEnumerator Glow()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && GlowingEffectMaterial != null) //�г�Ƽ ��忡�� ���� ��ġ�� ���� ���� �˷��ֱ� ���� ����Ʈ(��¦��)
        {
            renderer.material = GlowingEffectMaterial;
            yield return new WaitForSeconds(0.2f); //��¦�� ���ӽð�
        }

        if (renderer != null && originalMaterial != null) //������ ���׸��� ����
        {
            renderer.material = originalMaterial;
        }
    }
}
