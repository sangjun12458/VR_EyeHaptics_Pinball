using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Transform selectedObject;
    private Color selectedColor = Color.blue;

    // ���õ� ������Ʈ�� ũ�� �� ������ �����ϴ� �޼���
    public void SetSelectedObject(GameObject obj)
    {
        selectedObject = obj.transform;
        // ���õ� ������Ʈ�� ���� �ʱ�ȭ
        selectedColor = selectedObject.GetComponent<Renderer>().material.color;
    }

    public void ChangeSize(float size)
    {
        if (selectedObject != null)
        {
            selectedObject.localScale = Vector3.one * size;
        }
    }

    public void ChangeColor(float r, float g, float b)
    {
        if (selectedObject != null)
        {
            selectedColor = new Color(r, g, b);
            selectedObject.GetComponent<Renderer>().material.color = selectedColor;
        }
    }

    public void ResetColor()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material.color = selectedColor;
        }
    }
}
