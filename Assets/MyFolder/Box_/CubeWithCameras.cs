using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWithCameras : MonoBehaviour
{
    public Camera[] faceCameras; // 큐브의 각 면에 대한 카메라 배열

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
            // 큐브의 각 면에 카메라를 배치합니다.
            Vector3 facePosition = transform.position;
            Quaternion faceRotation = Quaternion.identity;

            switch (i)
            {
                case 0: // 앞면
                    facePosition += transform.forward * 5f;
                    break;
                case 1: // 뒷면
                    facePosition += transform.forward * -5f;
                    faceRotation = Quaternion.Euler(0f, 180f, 0f);
                    break;
                case 2: // 왼쪽 면
                    facePosition += transform.right * -5f;
                    faceRotation = Quaternion.Euler(0f, -90f, 0f);
                    break;
                case 3: // 오른쪽 면
                    facePosition += transform.right * 5f;
                    faceRotation = Quaternion.Euler(0f, 90f, 0f);
                    break;
                case 4: // 위쪽 면
                    facePosition += transform.up * 5f;
                    faceRotation = Quaternion.Euler(-90f, 0f, 0f);
                    break;
                case 5: // 아래쪽 면
                    facePosition += transform.up * -5f;
                    faceRotation = Quaternion.Euler(90f, 0f, 0f);
                    break;
            }

            // 각 면의 카메라 위치와 회전을 설정합니다.
            faceCameras[i].transform.position = facePosition;
            faceCameras[i].transform.rotation = faceRotation;
        }
    }
}
