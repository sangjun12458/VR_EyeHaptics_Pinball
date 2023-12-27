using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int TotalScore = 0;

    private void Update()
    {
        //Debug.Log(TotalScore);
    }

    void OnCollisionEnter(Collision collision)
    {
        string collisionObjectTag = collision.gameObject.tag;

        if (collisionObjectTag == "Pump" && Fever_Penalty.isFever == true)
        {
            TotalScore += 1000;
        }
        else if (collisionObjectTag == "Pump" && Fever_Penalty.isFever == false)
        {
            TotalScore += 100;
        }
    }
}
