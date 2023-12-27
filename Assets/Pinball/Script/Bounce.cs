using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceForce = 10f;

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ����� Rigidbody�� ������ �ִٸ� �ݴ� �������� ���� ���Ѵ�.
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRigidbody != null)
        {
            // �浹�� ǥ���� ������ ���Ѵ�.
            Vector3 bounceDirection = -collision.contacts[0].normal;

            // �ݴ� �������� ���� ���Ѵ�.
            otherRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
