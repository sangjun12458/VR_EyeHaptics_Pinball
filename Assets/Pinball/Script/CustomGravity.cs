using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    // 씬마다 다른 중력을 설정하려면 Inspector에서 설정할 값을 사용할 수 있습니다.
    public Vector3 customGravity = new Vector3(0f, -8.81f, -0f);

    void Start()
    {
        // 씬 시작 시 중력을 설정합니다.
        Physics.gravity = customGravity;
    }
}
