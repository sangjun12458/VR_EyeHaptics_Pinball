using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class Fever_Penalty: MonoBehaviour, IGazeFocusable
{
    public static bool isDark = false; //���� �� �����ϸ� ����Ʈ Ȱ��ȭ�� �� ��� �˾Ƽ� �ٲ�.
    public static bool isFever = false;

    public Material originalMaterial;
    public Material GlowingEffectMaterial;

    //�ӽ�
    bool is_been_seen = false;
    private float totalFeverDuration = 0f;
    [SerializeField] private float feverTimeThreshold = 3f;

    public Transform feverBarTransform;
    private Transform bar;
    private float feverStartTime;
    public float feverDuration = 10f; //�ǹ� ��� ���� �ð� (��)
    public float penaltyDuration = 15f; //�г�Ƽ ���ӽð� (��)_
    private float timeSinceLastSeen; // ���������� seen�� true�� �������� ��� �ð�
    public float delayBeforeFeverEnds = 1f; // seen�� false�� ���¸� �����ϱ������ �ð� (��)

    [Header("VR and Eye-Track")]
    private bool _hasFocus = false;

    void Start()
    {
        bar = feverBarTransform.Find("Fever");

        if (bar != null)
        {
            bar.localScale = new Vector3(0f, 1f, 1f);
        }
        else
        {
            Debug.LogError("Could not find 'Fever' object in the hierarchy.");
        }

        StartCoroutine(AdjustFeverPeriodically());
        StartCoroutine(FeverGlow()); //�ǹ�����϶� ���� ����
    }

    void Update()
    {
    }


    IEnumerator FeverGlow()
    {
        while (true)
        {
            if (isFever == true)
            {
                Renderer renderer = GetComponent<Renderer>();

                if (renderer != null && GlowingEffectMaterial != null) //�ǹ� �ð����� ���� ��¦��
                {
                    renderer.material = GlowingEffectMaterial;
                }
            }
            else
            {
                if (GetComponent<Renderer>() != null && originalMaterial != null) //�ǹ��� ������ �ٽ� ������
                {
                    GetComponent<Renderer>().material = originalMaterial;
                }
            }

            yield return null;
        }
    }

    IEnumerator AdjustFeverPeriodically()
    {
        while (true)
        {
            if (ResetBall.isDead == true) //
            {
                // �ǹ����/�г�Ƽ��� ���� �� ������ �ʱ�ȭ
                bar.localScale = new Vector3(0f, 1f, 1f);
                isFever = false;
                isDark = false;
                ResetBall.isDead = false;
            }
            else if (!PlungerScript.ballReady) // Plunger�� ���� Ready�����̸� �۵� x
            {
                if (isFever)
                {
                    // seen�� false�� ���°� 0.5�� �̻� ���ӵǸ� �ǹ� ��� ����
                    if (!is_been_seen) //���� �ٶ󺸰� ���� ������
                    {
                        timeSinceLastSeen += Time.deltaTime;

                        if (timeSinceLastSeen >= delayBeforeFeverEnds) // 0.5���� �����ð�
                        {
                            // �ǹ� ��� ���� �� ������ �ʱ�ȭ
                            bar.localScale = new Vector3(0f, 1f, 1f);
                            isFever = false;
                            isDark = true; //�г�Ƽ �۵�

                            // ������ Ȱ��ȭ���� ��� ���
                            float penaltyStartTime = Time.time;
                            while (Time.time - penaltyStartTime < penaltyDuration && ResetBall.isDead == false) //isDead�� true�� ������
                            {
                                yield return null; // �� ������ ���
                            }
                            //�г�Ƽ ���ӽð� 15��

                            isDark = false;
                        }
                    }
                    else
                    {
                        timeSinceLastSeen = 0f; // seen�� true�� ���� ��� �ð� �ʱ�ȭ
                    }

                    // �ǹ� ��尡 ������ �� ���� �ð��� ����ϸ� ����
                    if (Time.time - feverStartTime >= feverDuration)
                    {
                        // �ǹ� ��� ���� �� ������ �ʱ�ȭ
                        bar.localScale = new Vector3(0f, 1f, 1f);
                        isFever = false;

                        // ������ Ȱ��ȭ���� ��� ���
                        yield return new WaitForSeconds(1f);
                    }
                }
                else if (isDark == false) //�г�Ƽ �۵������� �������� �ǹ������� �۵�
                {
                    // is_seen ���� ���� �������� ����
                    if (is_been_seen)
                    {
                        // �ִ� �������� �����ϸ� �ǹ� ��� ����
                        bar.localScale += new Vector3(0.5f, 0f, 0f) * Time.deltaTime;

                        if (bar.localScale.x >= 1f)
                        {
                            StartFeverMode();
                        }
                    }
                    else
                    {
                        // �������� õõ�� �پ��� ����
                        bar.localScale -= new Vector3(0.1f, 0f, 0f) * Time.deltaTime;
                    }

                    // ũ�⸦ 0���� 1�� ����
                    bar.localScale = new Vector3(Mathf.Clamp(bar.localScale.x, 0f, 1f), 1f, 1f);
                }
            }


            yield return null; // �� ������ ���
        }
    }

    // �ǹ� ��带 �����ϴ� �Լ�
    public void StartFeverMode()
    {
        if (!isFever)
        {
            isFever = true;
            feverStartTime = Time.time;
        }
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        //_hasFocus = hasFocus;
        is_been_seen = hasFocus;
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
        }
    }
}
