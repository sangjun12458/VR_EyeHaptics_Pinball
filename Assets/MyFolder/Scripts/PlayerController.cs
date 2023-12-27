using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Tobii.XR;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Transform cameraTransform;
    public float rotationSpeed = 5f; // 회전 속도

    private Vector3 moveDirection = new Vector3();
    private Vector3 inputDirection = new Vector3();

    public GameObject sceneMenu;
    private bool canToggleSceneMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();

        if (ControllerManager.Instance.GetButtonPress(ControllerButton.Menu))
        {
            if (sceneMenu != null && canToggleSceneMenu) 
            {
                sceneMenu.SetActive(!sceneMenu.activeSelf);
                canToggleSceneMenu = false;
                Invoke("ResetCanToggleSceneMenu", 0.5f);
            }
        }
    }

    void ResetCanToggleSceneMenu()
    {
        canToggleSceneMenu = true;
    }

    void ControlPlayer()
    {
        if (ControllerManager.Instance.GetButtonPress(ControllerButton.Touchpad))
        {
            float horizontalInput = ControllerManager.Instance.GetTouchpadAxis().x;
            float verticalInput = ControllerManager.Instance.GetTouchpadAxis().y;
            horizontalInput = Mathf.Abs(horizontalInput) > 0.9f ? horizontalInput : 0f;
            verticalInput = Mathf.Abs(verticalInput) > 0.9f ? verticalInput : 0f;

            // 이동 방향 계산
            Vector3 moveDirection = new Vector3(0f, 0f, verticalInput).normalized;
            Vector3 forward = cameraTransform.forward;
            forward.y = 0f;
            forward.Normalize();
            Vector3 right = cameraTransform.right;
            right.y = 0f;
            right.Normalize();

            Vector3 desiredMoveDirection = forward * moveDirection.z + right * moveDirection.x;

            // 현재 바라보는 방향으로 이동
            transform.Translate(desiredMoveDirection * moveSpeed * Time.deltaTime, Space.World);

            // 회전 각도 계산
            float rotationAmount = horizontalInput;

            // 현재 오브젝트를 회전시킴
            transform.Rotate(0f, rotationAmount, 0f);
        }
        /*        if (ControllerManager.Instance.GetButtonTouch(ControllerButton.Touchpad))
                {
                    float verticalInput = ControllerManager.Instance.GetTouchpadAxis().x;
                    float horizontalInput = ControllerManager.Instance.GetTouchpadAxis().y;
                    inputDirection = new Vector3(horizontalInput, 0.0f, 0.0f);

                    if (cameraTransform != null)
                    {
                        moveDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * inputDirection;
                    }

                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                }

                if (ControllerManager.Instance.GetButtonPressDown(ControllerButton.Touchpad))
                {
                    float horizontalInput = ControllerManager.Instance.GetTouchpadAxis().x;
                    float verticalInput = ControllerManager.Instance.GetTouchpadAxis().y;

                    // 회전 각도 계산
                    float rotationAmount = horizontalInput * rotationSpeed;

                    // 현재 오브젝트를 회전시킴
                    transform.Rotate(Vector3.up, rotationAmount);
                }*/
    }

    void Temp()
    {/*
        if (ControllerManager.Instance.GetButtonPressDown(ControllerButton.Touchpad) && _hasFocus)
        {
            //OnPressedDown();
        }

        // If the touchpad button is released when the toggle is pressed, perform a click.
        if (ControllerManager.Instance.GetButtonPressUp(ControllerButton.Touchpad) && _buttonPressed)
        {
            //Toggle();
        }

        if (ControllerManager.Instance.GetButtonTouchDown(TouchpadButton))
        {
            //_padXLastFrame = ControllerManager.Instance.GetTouchpadAxis().x;
            //_sliderGraphics.StartHandleAnimation(true);
            //return;
        }

        // When the touchpad is being touched.
        if (ControllerManager.Instance.GetButtonTouch(TouchpadButton))
        {
            //UpdateSlider();
        }

        // When the touchpad is released.
        if (ControllerManager.Instance.GetButtonTouchUp(TouchpadButton))
        {
            //_sliderGraphics.StartHandleAnimation(false);
        }
        */
    }
}
