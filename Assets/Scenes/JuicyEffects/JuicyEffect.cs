using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class JuicyEffect : MonoBehaviour, IGazeFocusable
{
    [Header ("Bouncing")]
    private Rigidbody rb;
    public float bounceFactor = 0.8f; // 튕김 정도
    public float gravityScale = 1.0f; // 중력 강도
    public float airResistance = 0.02f; // 공기 저항

    [SerializeField]
    private Vector3 velocity;

    private AudioSource sound;

    public GameObject effect;

    private bool _hasFocus = false;

    private bool isGazed = false;

    private Vector3 originalPosition; // 오브젝트의 원래 위치 저장 변수
    private Quaternion originalRotation;
    public float resetInterval = 5.0f; // 오브젝트를 원래 위치로 돌아가는 간격

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();

        originalPosition = transform.position;
        originalRotation = transform.rotation;
         
        StartCoroutine(ResetObjectRepeatedly());

    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != new Vector3(0,0,0))
            velocity = rb.velocity;
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        _hasFocus = hasFocus;
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
            isGazed = true;
            effect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
            isGazed = false;
            effect.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 바닥인 경우
        if (collision.gameObject.CompareTag("Ground"))
        {
            Bounce();
            sound.Play();
            if (effect != null)
                effect.SetActive(false);
                effect.SetActive(true);
        }

    }

    void Bounce()
    {
        // 바닥에 닿았을 때의 튕김 동작을 구현합니다.
        Vector3 reflectedForce = -velocity * bounceFactor;
        rb.AddForce(reflectedForce, ForceMode.Impulse);
    }

    IEnumerator ResetObjectRepeatedly()
    {
        while (true)
        {
            // 일정 시간만큼 대기
            yield return new WaitForSeconds(resetInterval);

            // 오브젝트를 원래 위치로 돌림
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
