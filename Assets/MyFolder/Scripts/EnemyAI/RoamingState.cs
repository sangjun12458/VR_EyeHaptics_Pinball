using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingState : MonoBehaviour
{
    public float roamDuration = 5.0f; // ��ȸ ���� ���� �ð�
    public float roamSpeed = 2.0f; // �̵� �ӵ�

    private float currentTime = 0.0f;
    private Vector3 roamDirection;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ�ȭ �ڵ�
        GetRandomRoamDirection();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= roamDuration)
        {
            GetRandomRoamDirection();
            currentTime = 0.0f;
        }

        // �̵� �ڵ�
        transform.Translate(roamDirection * roamSpeed * Time.deltaTime);

    }

    private void GetRandomRoamDirection()
    {
        // �������� �̵� ���� ����
        roamDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
