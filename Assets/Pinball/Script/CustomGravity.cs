using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    // ������ �ٸ� �߷��� �����Ϸ��� Inspector���� ������ ���� ����� �� �ֽ��ϴ�.
    public Vector3 customGravity = new Vector3(0f, -8.81f, -0f);

    void Start()
    {
        // �� ���� �� �߷��� �����մϴ�.
        Physics.gravity = customGravity;
    }
}
