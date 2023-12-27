using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class Fever_Penalty: MonoBehaviour, IGazeFocusable
{
    public static bool isDark = false; //여기 값 수정하면 이펙트 활성화랑 맵 밝기 알아서 바뀜.
    public static bool isFever = false;

    public Material originalMaterial;
    public Material GlowingEffectMaterial;

    //임시
    bool is_been_seen = false;
    private float totalFeverDuration = 0f;
    [SerializeField] private float feverTimeThreshold = 3f;

    public Transform feverBarTransform;
    private Transform bar;
    private float feverStartTime;
    public float feverDuration = 10f; //피버 모드 지속 시간 (초)
    public float penaltyDuration = 15f; //패널티 지속시간 (초)_
    private float timeSinceLastSeen; // 마지막으로 seen이 true일 때부터의 경과 시간
    public float delayBeforeFeverEnds = 1f; // seen이 false인 상태를 인정하기까지의 시간 (초)

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
        StartCoroutine(FeverGlow()); //피버모드일때 공이 빛남
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

                if (renderer != null && GlowingEffectMaterial != null) //피버 시간동안 공이 반짝임
                {
                    renderer.material = GlowingEffectMaterial;
                }
            }
            else
            {
                if (GetComponent<Renderer>() != null && originalMaterial != null) //피버가 끝나면 다시 원상태
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
                // 피버모드/패널티모드 종료 후 게이지 초기화
                bar.localScale = new Vector3(0f, 1f, 1f);
                isFever = false;
                isDark = false;
                ResetBall.isDead = false;
            }
            else if (!PlungerScript.ballReady) // Plunger에 공이 Ready상태이면 작동 x
            {
                if (isFever)
                {
                    // seen이 false인 상태가 0.5초 이상 지속되면 피버 모드 종료
                    if (!is_been_seen) //공을 바라보고 있지 않을때
                    {
                        timeSinceLastSeen += Time.deltaTime;

                        if (timeSinceLastSeen >= delayBeforeFeverEnds) // 0.5초의 유예시간
                        {
                            // 피버 모드 종료 후 게이지 초기화
                            bar.localScale = new Vector3(0f, 1f, 1f);
                            isFever = false;
                            isDark = true; //패널티 작동

                            // 게이지 활성화까지 잠시 대기
                            float penaltyStartTime = Time.time;
                            while (Time.time - penaltyStartTime < penaltyDuration && ResetBall.isDead == false) //isDead가 true면 나가짐
                            {
                                yield return null; // 한 프레임 대기
                            }
                            //패널티 지속시간 15초

                            isDark = false;
                        }
                    }
                    else
                    {
                        timeSinceLastSeen = 0f; // seen이 true일 때는 경과 시간 초기화
                    }

                    // 피버 모드가 시작한 후 일정 시간이 경과하면 종료
                    if (Time.time - feverStartTime >= feverDuration)
                    {
                        // 피버 모드 종료 후 게이지 초기화
                        bar.localScale = new Vector3(0f, 1f, 1f);
                        isFever = false;

                        // 게이지 활성화까지 잠시 대기
                        yield return new WaitForSeconds(1f);
                    }
                }
                else if (isDark == false) //패널티 작동중이지 않을때만 피버게이지 작동
                {
                    // is_seen 값에 따라 게이지를 조절
                    if (is_been_seen)
                    {
                        // 최대 게이지에 도달하면 피버 모드 시작
                        bar.localScale += new Vector3(0.5f, 0f, 0f) * Time.deltaTime;

                        if (bar.localScale.x >= 1f)
                        {
                            StartFeverMode();
                        }
                    }
                    else
                    {
                        // 게이지가 천천히 줄어드는 로직
                        bar.localScale -= new Vector3(0.1f, 0f, 0f) * Time.deltaTime;
                    }

                    // 크기를 0에서 1로 제한
                    bar.localScale = new Vector3(Mathf.Clamp(bar.localScale.x, 0f, 1f), 1f, 1f);
                }
            }


            yield return null; // 한 프레임 대기
        }
    }

    // 피버 모드를 시작하는 함수
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
