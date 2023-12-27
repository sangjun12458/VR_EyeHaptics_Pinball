using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        float moveForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveSideways = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 movement = transform.forward * moveForward + transform.right * moveSideways;

        transform.Translate(movement, Space.World);
    }
}
