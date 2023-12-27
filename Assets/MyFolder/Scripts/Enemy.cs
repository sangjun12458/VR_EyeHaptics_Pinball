using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 4.0f;  // 적의 이동 속도
    public float stoppingDistance = 100.0f;  // 플레이어를 멈출 거리

    public Transform player;

    private Camera mainCamera;  // 메인 카메라


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = screenPos.z;  // 적의 Z 좌표를 마우스의 Z 좌표로 설정
        float distanceToMouse = Vector3.Distance(screenPos, mousePosition);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToMouse > stoppingDistance && distanceToPlayer > 3)
        {
            // 플레이어 방향을 향하도록 회전
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            // 플레이어 쪽으로 이동
            transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
        }
    }
}
