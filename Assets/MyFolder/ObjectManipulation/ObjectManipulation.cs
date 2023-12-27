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
        // 초기 크기 설정
        sizeSlider.value = targetObject.transform.localScale.x;

        // 슬라이더 리스너 등록
        sizeSlider.onValueChanged.AddListener(ChangeObjectSize);

    }

    // Update is called once per frame
    void Update()
    {
        ChangeObjectColor(colorPicker.color);
    }

    // 크기 변경 함수
    void ChangeObjectSize(float newSize)
    {
        targetObject.transform.localScale = new Vector3(newSize, newSize, newSize);
    }

    // 색상 변경 함수
    void ChangeObjectColor(Color newColor)
    {
        targetObject.GetComponent<Renderer>().material.color = newColor;
    }


    // 컬러 피커에서 색상 선택 시 호출되는 함수
    void OnColorPicked(Color newColor)
    {
        ChangeObjectColor(newColor);
    }
}
