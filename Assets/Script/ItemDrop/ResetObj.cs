using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObj : MonoBehaviour
{
    public float resetDelay = 3.0f; // 오브젝트를 원래 위치로 돌리기까지의 지연 시간
    public float resetInterval = 5.0f; // 오브젝트를 원래 위치로 돌아가는 간격

    private Vector3 originalPosition; // 오브젝트의 원래 위치 저장 변수
    private Quaternion originalRotation;

    void Start()
    {
        // 초기에 오브젝트의 원래 위치를 저장
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // 일정 시간 간격으로 오브젝트를 원래 위치로 돌리는 Coroutine 호출
        StartCoroutine(ResetObjectRepeatedly());
    }

    IEnumerator ResetObjectRepeatedly()
    {
        while (true)
        {
            // 일정 시간만큼 대기
            yield return new WaitForSeconds(resetInterval);

            // 오브젝트를 원래 위치로 돌림
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
