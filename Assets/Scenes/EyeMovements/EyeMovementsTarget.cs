using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class EyeMovementsTarget : MonoBehaviour, IGazeFocusable
{
    private enum EyeBehavior
    {
        Fixation,
        Saccade,
        SmoothPursuit
    }
    [SerializeField]
    private EyeBehavior eyeBehavior;
    private bool _hasFocus = false;

    private bool isTargeted = false;
    private float fixationTime = 0.6f; //50~600ms
    private float saccadeTime = 1f; //계획 100~1000ms 지속 20-40ms
    private float smoothPursuitTime = 3f;
    private float smoothPursuitTimer = 0f;
    private float smoothPursuitSpeed = 3f; //30deg/sec 미만일 때 부드러운 추적. 이상이면 단속운동. 최대는 100deg/sec
    private float cumulativeTime = 0f;
    private float timer = 0f;

    public EyeMovementsManager manager;

    private Color targetColor = Color.red;
    private Color initialColor;
    private Color smoothPursuitColor = Color.blue;

    private float minX = -6f;
    private float maxX = 6f;
    private float minY = 2f;
    private float maxY = 7f;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
        movingRight = Random.Range(0, 1) > 0.5 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (_hasFocus) 
        {
            cumulativeTime += Time.deltaTime;
            if (cumulativeTime >= fixationTime)
                isTargeted = true;

            Color lerpedColor = Color.Lerp(initialColor, targetColor, Mathf.Clamp01(cumulativeTime / fixationTime));
            GetComponent<Renderer>().material.color = lerpedColor;
        }
        else
        {
            cumulativeTime = 0f;
            isTargeted = false;

            GetComponent<Renderer>().material.color = initialColor;
        }

        switch (eyeBehavior)
        {
            case EyeBehavior.Fixation:
                if (isTargeted)
                {
                    manager.targetsRemaining--;
                    DestroyTarget();
                }
                timer = 0f;
                break;

            case EyeBehavior.Saccade:
                if (isTargeted)
                {
                    manager.targetsRemaining--;
                    if (manager.targetsRemaining > 0)
                        manager.SpawnTarget();
                    DestroyTarget();
                }
                if (timer >= saccadeTime)
                {
                    MoveToRandomPosition();
                    timer = 0f;
                }
                break;

            case EyeBehavior.SmoothPursuit:
                timer = 0f;
                if (isTargeted)
                {
                    smoothPursuitTimer += Time.deltaTime;
                    Color spColor = Color.Lerp(targetColor, smoothPursuitColor, Mathf.Clamp01(smoothPursuitTimer / smoothPursuitTime));
                    GetComponent<Renderer>().material.color = spColor;
                    if (smoothPursuitTimer > smoothPursuitTime)
                    {
                        manager.targetsRemaining--;
                        if (manager.targetsRemaining > 0)
                            manager.SpawnTarget();
                        DestroyTarget();
                    }
                }
                else
                {
                    smoothPursuitTimer = Mathf.Max(0, smoothPursuitTimer - Time.deltaTime * 2);
                }
                // 오른쪽으로 이동 중이면서 오른쪽 경계에 도달하면 방향 변경
                if (movingRight && transform.position.x >= maxX)
                {
                    movingRight = false;
                }
                // 왼쪽으로 이동 중이면서 왼쪽 경계에 도달하면 방향 변경
                else if (!movingRight && transform.position.x <= minX)
                {
                    movingRight = true;
                }

                // 이동 방향에 따라 이동
                float direction = movingRight ? 1f : -1f;
                transform.Translate(Vector3.right * direction * smoothPursuitSpeed * Time.deltaTime);
                break;

            default: break;
        }
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        _hasFocus = hasFocus;
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
        }
    }

    public void DestroyTarget()
    {
        manager.TargetDestroyed();
        Destroy(gameObject);
    }

    void MoveToRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);
        transform.position = newPosition;
    }
}
