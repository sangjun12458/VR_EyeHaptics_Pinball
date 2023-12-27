using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Life : MonoBehaviour
{
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = ResetBall.life;
    }
}
