using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetBall : MonoBehaviour
{
    public GameObject ballObject;
    public float x, y, z;

    public static bool isDead; //���� �׾����� Ȯ��

    private Vector3 originalPosition; // ������Ʈ�� ���� ��ġ ���� ����
    private Quaternion originalRotation;

    private int lifeCount = 5;
    public static string life = "5";

    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = ballObject.transform.position;
        originalRotation = ballObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            isDead = true;

            lifeCount -= 1;
            if (lifeCount < 0)
            {
                life = "Game Over";
                return;
            }
            life = lifeCount.ToString();

            other.gameObject.transform.position = originalPosition;
            other.gameObject.transform.rotation = originalRotation;


        }
    }
}
