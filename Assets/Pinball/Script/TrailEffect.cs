using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;


public class TrailEffect : MonoBehaviour, IGazeFocusable
{
    public GameObject trailPrefab; // �ܻ� ������Ʈ ������
    public float trailInterval = 0.1f; // �ܻ� ���� ���� (��)
    public float trailFadeSpeed = 2.0f; // �ܻ� ���� ���� �ӵ�

    private float lastTrailTime;
    private Transform trailContainer; // ������ �ܻ��� ���� �θ� �����̳�

    [Header("VR and Eyetrack")]
    private bool _hasFocus = false;

    void Start()
    {
        // �θ� �����̳� ����
        trailContainer = new GameObject("TrailContainer").transform;
    }

    void Update()
    {
        // Get the magnitude of the velocity
        float velocityMagnitude = GetComponent<Rigidbody>().velocity.magnitude;

        // Calculate a new trail interval based on velocity
        float adjustedTrailInterval = Mathf.Lerp(0.5f, 0.05f, velocityMagnitude / 10.0f);

        // ��ü�� ������ ��
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
        {
            if (!_hasFocus) return;

            // ���� �������� �ܻ� ����
            if (Time.time - lastTrailTime > adjustedTrailInterval)
            {
                CreateTrail();
                lastTrailTime = Time.time;
            }
        }
    }

    void CreateTrail()
    {
        // �ܻ� ������Ʈ ����
        GameObject trailObject = Instantiate(trailPrefab, transform.position, transform.rotation, trailContainer);

        // ���� ���� �ڷ�ƾ ����
        StartCoroutine(FadeTrail(trailObject.GetComponent<Renderer>()));
    }

    IEnumerator FadeTrail(Renderer trailRenderer)
    {
        // �ʱ� ����
        Color startColor = trailRenderer.material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;
        float fadeDuration = trailFadeSpeed / 4.0f;

        while (elapsedTime < fadeDuration)
        {
            // ���� ����
            trailRenderer.material.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ ������ ������� ������Ʈ ����
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
