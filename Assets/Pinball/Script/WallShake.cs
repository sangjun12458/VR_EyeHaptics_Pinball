using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShake : MonoBehaviour
{
    public float shakeIntensity = 0.1f; // ��鸲 ����
    public float shakeDuration = 0.2f; // ���ӽð�
    public float minCollisionSpeed = 5.0f; // ���� �ӵ� �̻��� ���� ��鸲 �߻�

    private Vector3 originalPosition;

    public Material originalMaterial;
    public Material GlowingEffectMaterial;

    void Start()
    {
        // ��ü�� ���� ��ġ ����
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        //���� ��ü�� �ٶ󺸰� �ִ��� Ȯ���ϴ� �ڵ��Է��ϱ�

        // ��� �ӵ� ����
        float collisionSpeed = collision.relativeVelocity.magnitude;

        // �ӵ�Ȯ��
        if (collisionSpeed > minCollisionSpeed && collision.gameObject.CompareTag("Ball") && CollisionEffect._hasFocus)
        {
            // ��ü ����
            StartCoroutine(Shake());

            // ��Ʈ�ѷ����� ���� �ֱ�
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

        // ��鸲 ���� ��ġ �ʱ�ȭ
        transform.position = originalPosition;
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