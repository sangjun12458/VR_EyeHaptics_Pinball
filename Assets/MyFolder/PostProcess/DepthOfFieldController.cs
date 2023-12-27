using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class DepthOfFieldController : MonoBehaviour
{
    public LayerMask blurLayerMask;  // 블러 처리를 적용할 레이어
    public Camera mainCamera;
    private PostProcessProfile postProcessProfile;
    private bool isMouseOverObject;


    // Start is called before the first frame update
    void Start()
    {
        postProcessProfile = GetComponent<PostProcessVolume>().profile;

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 마우스를 통해 가리키는 오브젝트의 레이어 확인
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == blurLayerMask)
            {
                isMouseOverObject = true;
            }
            else
            {
                isMouseOverObject = false;
            }
        }

        // 블러 처리 여부에 따라 프로파일 업데이트
        if (isMouseOverObject)
        {
            // 블러 처리를 적용하지 않음
            postProcessProfile.GetSetting<DepthOfField>().active = false;
        }
        else
        {
            // 블러 처리를 적용
            postProcessProfile.GetSetting<DepthOfField>().active = true;
        }
    }
}
