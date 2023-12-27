using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class DepthOfFieldController : MonoBehaviour
{
    public LayerMask blurLayerMask;  // �� ó���� ������ ���̾�
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

        // ���콺�� ���� ����Ű�� ������Ʈ�� ���̾� Ȯ��
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

        // �� ó�� ���ο� ���� �������� ������Ʈ
        if (isMouseOverObject)
        {
            // �� ó���� �������� ����
            postProcessProfile.GetSetting<DepthOfField>().active = false;
        }
        else
        {
            // �� ó���� ����
            postProcessProfile.GetSetting<DepthOfField>().active = true;
        }
    }
}
