using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray_DetectCollision : MonoBehaviour
{

    void Update()
    {
        // 마우스 위치를 화면 좌표로 얻어옵니다.
        Vector3 mousePosition = Input.mousePosition;

        // 마우스 위치를 월드 좌표로 변환합니다.
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)){
            // Check if the hit object is not null
            if (hit.collider != null){
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.gameObject.tag == "Objwatching"){
                    // Check for collisions between the hitObject and other objects
                    Collider hitCollider = hitObject.GetComponent<Collider>();

                    if (hitCollider != null){
                        // Check if there's a collision with other objects
                        Collider[] colliders = Physics.OverlapBox(hitCollider.bounds.center, hitCollider.bounds.extents, hitCollider.transform.rotation);

                        foreach (var collider in colliders){
                            if (collider.gameObject != hitObject && collider.gameObject.tag == "CollidingObject"){
                                Debug.Log("Collision between hitObject and " + collider.gameObject.name);
                                RumbleManager.instance.RumblePulse(1f, 1f, 0.25f);
                            }
                        }
                    }
                }
            }
        }
    }
}
