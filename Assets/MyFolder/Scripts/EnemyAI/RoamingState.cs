using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingState : MonoBehaviour
{
    public float roamDuration = 5.0f; // 배회 상태 지속 시간
    public float roamSpeed = 2.0f; // 이동 속도

    private float currentTime = 0.0f;
    private Vector3 roamDirection;

    // Start is called before the first frame update
    void Start()
    {
        // 초기화 코드
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

        // 이동 코드
        transform.Translate(roamDirection * roamSpeed * Time.deltaTime);

    }

    private void GetRandomRoamDirection()
    {
        // 무작위로 이동 방향 설정
        roamDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
