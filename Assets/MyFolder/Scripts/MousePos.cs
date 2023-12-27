using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DebugRaycasting();
    }

    void DebugMousePos()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                string text = mousePos.ToString();
                Debug.Log(text);
            }
        }
    }

    void DebugRaycasting()
    {

        if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // 마우스 위치에서 레이 생성
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                // 레이가 오브젝트에 부딪혔을 때
                Debug.Log("Hit at: " + hitInfo.point);
                Debug.Log("Hit object name: " + hitInfo.collider.gameObject.name);

            }
        }
    }

}


