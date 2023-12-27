using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWithCameras : MonoBehaviour
{
    public Camera[] faceCameras; // ť���� �� �鿡 ���� ī�޶� �迭

    // Start is called before the first frame update
    void Start()
    {
        InitializeCameras();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeCameras()
    {
        for (int i = 0; i < faceCameras.Length; i++)
        {
            // ť���� �� �鿡 ī�޶� ��ġ�մϴ�.
            Vector3 facePosition = transform.position;
            Quaternion faceRotation = Quaternion.identity;

            switch (i)
            {
                case 0: // �ո�
                    facePosition += transform.forward * 5f;
                    break;
                case 1: // �޸�
                    facePosition += transform.forward * -5f;
                    faceRotation = Quaternion.Euler(0f, 180f, 0f);
                    break;
                case 2: // ���� ��
                    facePosition += transform.right * -5f;
                    faceRotation = Quaternion.Euler(0f, -90f, 0f);
                    break;
                case 3: // ������ ��
                    facePosition += transform.right * 5f;
                    faceRotation = Quaternion.Euler(0f, 90f, 0f);
                    break;
                case 4: // ���� ��
                    facePosition += transform.up * 5f;
                    faceRotation = Quaternion.Euler(-90f, 0f, 0f);
                    break;
                case 5: // �Ʒ��� ��
                    facePosition += transform.up * -5f;
                    faceRotation = Quaternion.Euler(90f, 0f, 0f);
                    break;
            }

            // �� ���� ī�޶� ��ġ�� ȸ���� �����մϴ�.
            faceCameras[i].transform.position = facePosition;
            faceCameras[i].transform.rotation = faceRotation;
        }
    }
}
