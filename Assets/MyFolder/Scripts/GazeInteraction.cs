using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GazeInteraction : MonoBehaviour
{
    public GameObject detailUI; // ����� �������� ǥ���� UI ���
    public Text detailText; // ������ �ؽ�Ʈ�� ǥ���� UI Text ���

    public string detailString = "";

    public Material highlightMaterial; // ���̶���Ʈ�� ����� ���͸���

    private Material originalMaterial; // ������ ���͸���

    public Outline outlineScript;

    public float timeToDisplayInfo = 2.0f; // ����� ����Ű�� �־�� �ϴ� �ð�
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

        originalMaterial = GetComponent<Renderer>().material; // ������Ʈ�� ���� ���͸��� ����

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
                // ���콺�� ������Ʈ ���� ���� �� �������� ������Ʈ�ϰ� UI�� Ȱ��ȭ�մϴ�.
                detailText.text = detailString; // ����� �� ������ ����
                detailUI.transform.position = Camera.main.WorldToScreenPoint(transform.position); // ��� ��ġ�� UI�� ǥ��
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

        // ���콺�� ������Ʈ���� ��� �� UI�� �ٽ� ��Ȱ��ȭ�մϴ�.
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
