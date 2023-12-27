using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManipulation : MonoBehaviour
{
    public GameObject targetObject;
    public Slider sizeSlider;
    public FlexibleColorPicker colorPicker;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ũ�� ����
        sizeSlider.value = targetObject.transform.localScale.x;

        // �����̴� ������ ���
        sizeSlider.onValueChanged.AddListener(ChangeObjectSize);

    }

    // Update is called once per frame
    void Update()
    {
        ChangeObjectColor(colorPicker.color);
    }

    // ũ�� ���� �Լ�
    void ChangeObjectSize(float newSize)
    {
        targetObject.transform.localScale = new Vector3(newSize, newSize, newSize);
    }

    // ���� ���� �Լ�
    void ChangeObjectColor(Color newColor)
    {
        targetObject.GetComponent<Renderer>().material.color = newColor;
    }


    // �÷� ��Ŀ���� ���� ���� �� ȣ��Ǵ� �Լ�
    void OnColorPicked(Color newColor)
    {
        ChangeObjectColor(newColor);
    }
}
