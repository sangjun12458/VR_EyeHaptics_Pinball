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

        if (Input.GetMouseButtonDown(0))  // ���콺 ���� ��ư Ŭ�� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // ���콺 ��ġ���� ���� ����
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                // ���̰� ������Ʈ�� �ε����� ��
                Debug.Log("Hit at: " + hitInfo.point);
                Debug.Log("Hit object name: " + hitInfo.collider.gameObject.name);

            }
        }
    }

}


