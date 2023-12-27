using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObj : MonoBehaviour
{
    public float resetDelay = 3.0f; // ������Ʈ�� ���� ��ġ�� ����������� ���� �ð�
    public float resetInterval = 5.0f; // ������Ʈ�� ���� ��ġ�� ���ư��� ����

    private Vector3 originalPosition; // ������Ʈ�� ���� ��ġ ���� ����
    private Quaternion originalRotation;

    void Start()
    {
        // �ʱ⿡ ������Ʈ�� ���� ��ġ�� ����
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // ���� �ð� �������� ������Ʈ�� ���� ��ġ�� ������ Coroutine ȣ��
        StartCoroutine(ResetObjectRepeatedly());
    }

    IEnumerator ResetObjectRepeatedly()
    {
        while (true)
        {
            // ���� �ð���ŭ ���
            yield return new WaitForSeconds(resetInterval);

            // ������Ʈ�� ���� ��ġ�� ����
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
