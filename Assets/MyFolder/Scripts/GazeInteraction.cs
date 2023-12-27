using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GazeInteraction : MonoBehaviour
{
    public GameObject detailUI; // 대상의 상세정보를 표시할 UI 요소
    public Text detailText; // 상세정보 텍스트를 표시할 UI Text 요소

    public string detailString = "";

    public Material highlightMaterial; // 하이라이트에 사용할 머터리얼

    private Material originalMaterial; // 원래의 머터리얼

    public Outline outlineScript;

    public float timeToDisplayInfo = 2.0f; // 대상을 가리키고 있어야 하는 시간
    private float currentTime = 0.0f;
    private bool isMouseOver = false;

    public enum Mode
    {
        Color,
        Line,
    }
    public Mode HighlightMode
    {
        get { return highlightMode; }
        set
        {
            highlightMode = value;
            needsUpdate = true;
        }
    }
    [SerializeField]
    private Mode highlightMode;

    private bool needsUpdate;

    // Start is called before the first frame update
    void Start()
    {
        if (detailUI != null)
            detailUI.SetActive(false);

        originalMaterial = GetComponent<Renderer>().material; // 오브젝트의 원래 머터리얼 저장

        if (outlineScript != null)
            outlineScript.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseOver)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeToDisplayInfo)
            {
                // 마우스가 오브젝트 위에 있을 때 상세정보를 업데이트하고 UI를 활성화합니다.
                detailText.text = detailString; // 대상의 상세 정보를 설정
                detailUI.transform.position = Camera.main.WorldToScreenPoint(transform.position); // 대상 위치에 UI를 표시
                detailUI.SetActive(true);
            }
        }
        else
        {
            currentTime = 0.0f;
        }
    }

    private void OnMouseOver()
    {
        if (detailUI == null)
            return;

        if (detailText == null)
            return;

        switch (highlightMode)
        {
            case Mode.Line:
                if (outlineScript == null)
                    return;
                outlineScript.enabled = true;
                break;

            case Mode.Color:
                GetComponent<Renderer>().material = highlightMaterial;
                break;
        }

        isMouseOver = true;

    }

    private void OnMouseExit()
    {
        if (detailUI == null)
            return;

        if (detailText == null)
            return;

        // 마우스가 오브젝트에서 벗어날 때 UI를 다시 비활성화합니다.
        isMouseOver = false;
        detailUI.SetActive(false);

        switch (highlightMode)
        {
            case Mode.Line:
                if (outlineScript == null)
                    return;
                outlineScript.enabled = false;
                break;

            case Mode.Color:
                GetComponent<Renderer>().material = originalMaterial;
                break;
        }
    }
}
