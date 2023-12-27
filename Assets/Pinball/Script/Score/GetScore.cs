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
        // TextBox GameObject���� TextMeshProUGUI ������Ʈ ��������
        textBox = GameObject.Find("Score").GetComponent<TextMeshPro>();
    }

    void Update()
    {
        //Debug.Log(Score.TotalScore);

        // �ؽ�Ʈ ������Ʈ ����: �� �����Ӹ��� �ð��� �ؽ�Ʈ�� ǥ��
        textBox.text = Score.TotalScore.ToString();
    }
}
