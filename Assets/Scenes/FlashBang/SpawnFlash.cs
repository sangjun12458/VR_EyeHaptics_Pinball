using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlash : MonoBehaviour
{
    public GameObject Flash;

    void Start()
    {
        StartCoroutine(ThrowFlash());
    }

    private void Update()
    {

    }

    IEnumerator ThrowFlash()
    {
        while(true)
        {
            Instantiate(Flash, transform);
            yield return new WaitForSeconds(8f);
        }
    }
}
