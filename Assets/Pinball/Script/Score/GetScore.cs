using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    public TextMeshPro textBox;

    // Start is called before the first frame update
    void Start()
    {
        // TextBox GameObject에서 TextMeshProUGUI 컴포넌트 가져오기
        textBox = GameObject.Find("Score").GetComponent<TextMeshPro>();
    }

    void Update()
    {
        //Debug.Log(Score.TotalScore);

        // 텍스트 업데이트 예제: 매 프레임마다 시간을 텍스트에 표시
        textBox.text = Score.TotalScore.ToString();
    }
}
