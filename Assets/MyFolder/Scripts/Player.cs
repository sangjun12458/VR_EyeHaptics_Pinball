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
        // �̵� �� ȸ�� �Է� ó��
        float HMInput = Input.GetAxis("HorizontalMovement");
        float VMInput = Input.GetAxis("VerticalMovement");
        float HRInput = Input.GetAxis("HorizontalRotation");
        float VRInput = Input.GetAxis("VerticalRotation");

        Vector3 moveDirection = new Vector3(HMInput, 0.0f, VMInput);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up * HRInput * rotationSpeed * Time.deltaTime);

        // ���� Ű�� ���� ȸ�� (���� ȸ���� ���� ī�޶� ��ü�� ȸ���̹Ƿ� LocalEulerAngles�� ���)
        float verticalRotation = VRInput * rotationSpeed * Time.deltaTime;
        float newRotationX = Mathf.Clamp(transform.localEulerAngles.x - verticalRotation, 0f, 80f); // ���� ȸ�� ���� ���� (0������ 80�� ���̷�)
        transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0f);

        // ���� �Է� ó��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // ���콺 �Է� ó��
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {

        }

        if (Input.GetMouseButtonDown(1))
        {
            // ���콺 ������ ��ư�� ������ ���� ������ ���⿡ �߰�
        }

        // UI ���� �� ��ȣ�ۿ��� Unity�� UI �̺�Ʈ �ý����� ����Ͽ� ó���� �� �ֽ��ϴ�.

        if (Input.GetKeyDown("m"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
