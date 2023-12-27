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

        if (renderer != null && GlowingEffectMaterial != null) //�г�Ƽ ��忡�� ���� ��ġ�� ���� ���� �˷��ֱ� ���� ����Ʈ(��¦��)
        {

            renderer.material = GlowingEffectMaterial;
            yield return new WaitWhile(() => PlungerScript.ballReady);
        }

        if (PlungerScript.ballReady == false) //������ ���׸��� ����
        {
            renderer.material = originalMaterial;
        }
    }
}
