using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceForce = 10f;

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 대상이 Rigidbody를 가지고 있다면 반대 방향으로 힘을 가한다.
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRigidbody != null)
        {
            // 충돌한 표면의 방향을 구한다.
            Vector3 bounceDirection = -collision.contacts[0].normal;

            // 반대 방향으로 힘을 가한다.
            otherRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
