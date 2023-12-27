using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class JuicyEffect : MonoBehaviour, IGazeFocusable
{
    [Header ("Bouncing")]
    private Rigidbody rb;
    public float bounceFactor = 0.8f; // ƨ�� ����
    public float gravityScale = 1.0f; // �߷� ����
    public float airResistance = 0.02f; // ���� ����

    [SerializeField]
    private Vector3 velocity;

    private AudioSource sound;

    public GameObject effect;

    private bool _hasFocus = false;

    private bool isGazed = false;

    private Vector3 originalPosition; // ������Ʈ�� ���� ��ġ ���� ����
    private Quaternion originalRotation;
    public float resetInterval = 5.0f; // ������Ʈ�� ���� ��ġ�� ���ư��� ����

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
        // �浹�� ������Ʈ�� �ٴ��� ���
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
        // �ٴڿ� ����� ���� ƨ�� ������ �����մϴ�.
        Vector3 reflectedForce = -velocity * bounceFactor;
        rb.AddForce(reflectedForce, ForceMode.Impulse);
    }

    IEnumerator ResetObjectRepeatedly()
    {
        while (true)
        {
            // ���� �ð���ŭ ���
            yield return new WaitForSeconds(resetInterval);

            // ������Ʈ�� ���� ��ġ�� ����
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
