using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnIndicator : MonoBehaviour
{
    public Material originalMaterial;
    public Material GlowingEffectMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        if (PlungerScript.ballReady == true)
            StartCoroutine(IndicateBall());
    }

    IEnumerator IndicateBall()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && GlowingEffectMaterial != null) //패널티 모드에서 공의 위치를 순간 순간 알려주기 위한 이펙트(반짝임)
        {

            renderer.material = GlowingEffectMaterial;
            yield return new WaitWhile(() => PlungerScript.ballReady);
        }

        if (PlungerScript.ballReady == false) //원래의 메테리얼 적용
        {
            renderer.material = originalMaterial;
        }
    }
}
