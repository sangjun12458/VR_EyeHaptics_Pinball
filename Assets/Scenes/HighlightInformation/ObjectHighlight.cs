using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.G2OM;


public class ObjectHighlight : MonoBehaviour, IGazeFocusable
{
    public GameObject detailUI; // ����� �������� ǥ���� UI ���
    public Text detailText; // ������ �ؽ�Ʈ�� ǥ���� UI Text ���
    public string detailString = "";

    public Outline outlineScript;

    public Material highlightMaterial; // ���̶���Ʈ�� ����� ���͸���
    private Material originalMaterial; // ������ ���͸���

    public float timeToDisplayInfo = 2.0f; // ����� ����Ű�� �־�� �ϴ� �ð�
    private float currentTime = 0.0f;

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
        }
    }
    [SerializeField]
    private Mode highlightMode;

    private bool _hasFocus = false;

    public void GazeFocusChanged(bool hasFocus)
    {
        _hasFocus = hasFocus;
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
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
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
            if (detailUI == null)
                return;

            if (detailText == null)
                return;

            // ���콺�� ������Ʈ���� ��� �� UI�� �ٽ� ��Ȱ��ȭ�մϴ�.
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

    // Start is called before the first frame update
    void Start()
    {
        if (detailUI != null)
            detailUI.SetActive(false);
        originalMaterial = GetComponent<Renderer>().material;

        if (outlineScript != null)
            outlineScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasFocus)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeToDisplayInfo)
            {
                // ���콺�� ������Ʈ ���� ���� �� �������� ������Ʈ�ϰ� UI�� Ȱ��ȭ�մϴ�.
                detailText.text = detailString; // ����� �� ������ ����
                //detailUI.transform.position = Camera.main.WorldToScreenPoint(transform.position); // ��� ��ġ�� UI�� ǥ��
                detailUI.SetActive(true);
            }
        }
        else
        {
            currentTime = 0.0f;
        }
    }
}
