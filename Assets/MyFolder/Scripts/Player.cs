using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 90.0f;
    public float jumpForce = 10.0f;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // 이동 및 회전 입력 처리
        float HMInput = Input.GetAxis("HorizontalMovement");
        float VMInput = Input.GetAxis("VerticalMovement");
        float HRInput = Input.GetAxis("HorizontalRotation");
        float VRInput = Input.GetAxis("VerticalRotation");

        Vector3 moveDirection = new Vector3(HMInput, 0.0f, VMInput);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up * HRInput * rotationSpeed * Time.deltaTime);

        // 상하 키로 상하 회전 (상하 회전은 보통 카메라 자체의 회전이므로 LocalEulerAngles를 사용)
        float verticalRotation = VRInput * rotationSpeed * Time.deltaTime;
        float newRotationX = Mathf.Clamp(transform.localEulerAngles.x - verticalRotation, 0f, 80f); // 상하 회전 각도 제한 (0도에서 80도 사이로)
        transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0f);

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 마우스 입력 처리
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {

        }

        if (Input.GetMouseButtonDown(1))
        {
            // 마우스 오른쪽 버튼을 눌렀을 때의 동작을 여기에 추가
        }

        // UI 선택 및 상호작용은 Unity의 UI 이벤트 시스템을 사용하여 처리할 수 있습니다.

        if (Input.GetKeyDown("m"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
