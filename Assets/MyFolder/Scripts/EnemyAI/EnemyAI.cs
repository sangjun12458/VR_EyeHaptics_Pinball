using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrolling,
        Hiding,
        Chasing,
        Fleeing,
        Dead
    }


    [Header ("State Info")]
    public EnemyState currentState;
    private Transform player;
    public float chaseDuration = 5.0f; // �߰� ���� �ð�
    public float fleeDuration = 3.0f;  // ���� ���� �ð�
    public float hideDuration = 3.0f;   // ���� ���� �ð�

    private float stateStartTime;

    public float enemyY = 0.0f;

    [Header("Eye Interaction Info")]
    public bool isInteractableObject = false;
    [SerializeField]
    private bool eyeInteractionEnabled = false;
    [SerializeField]
    private bool isGazeContact = false;
    [SerializeField]
    private float gazeContactTime = 0.0f;
    [Range(0.0f, 2.0f)]
    public float gazeTriggerTime;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.Patrolling;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateStartTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, enemyY, transform.position.z);

        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                if (CanSeePlayer())
                {
                    if (IsPlayerFacing())
                    {
                        SetState(EnemyState.Hiding);
                    }
                    else
                    {
                        SetState(EnemyState.Chasing);
                    }
                }
                break;

            case EnemyState.Hiding:
                Hide();
                if (Time.time - stateStartTime > hideDuration)
                {
                    if (CanSeePlayer())
                    {
                        if (IsPlayerFacing()) 
                        {
                            SetState(EnemyState.Fleeing);
                        }
                        else
                        {
                            SetState(EnemyState.Chasing);
                        }
                    }
                    else
                    {
                        SetState(EnemyState.Patrolling);
                    }
                }
                break;

            case EnemyState.Chasing:
                Chase();
                if (CanSeePlayer())
                {
                    if (IsPlayerFacing())
                    {
                        SetState(EnemyState.Fleeing);
                    }
                    else
                    {
                        if (Time.time - stateStartTime > chaseDuration)
                        {
                            SetState(EnemyState.Hiding);
                        }
                    }
                }
                else
                {
                    SetState(EnemyState.Patrolling);
                }
                break;

            case EnemyState.Fleeing:
                Flee();
                if (Time.time - stateStartTime > chaseDuration)
                {
                    SetState(EnemyState.Patrolling);
                }
                break;

            case EnemyState.Dead:
                // Handle the death state
                break;
        }

    }

    // �ٸ� ��ũ��Ʈ�� ��Ȳ���� ���¸� �����ϴ� �Լ��� ����ϴ�.
    public void SetState(EnemyState newState)
    {
        currentState = newState;
        stateStartTime = Time.time;
    }

    bool CanSeePlayer()
    {
        // �÷��̾� ���� ���� ����
        float detectionRange = 10.0f; // ���ϴ� ���� ���� ����

        if (player == null)
        {
            return false; // �÷��̾� ��ü�� ������ ���� �Ұ���
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        return distanceToPlayer <= detectionRange;
    }

    bool IsPlayerFacing()
    {
        if (isGazeContact)
        {
            gazeContactTime += Time.deltaTime;
            if (gazeContactTime >= gazeTriggerTime)
            {
                return true;
            }
        }
        return false;
    }

    void Patrol()
    {
        // ��ȸ ���� ����
        transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime);

    }

    void Hide()
    {
        // ���� ���� ����
        Vector3 hidePosition = new Vector3(10f, 0f, 10f); // ���� ��ġ
        transform.position = Vector3.MoveTowards(transform.position, hidePosition, Time.deltaTime);
    }

    void Chase()
    {
        // �߰� ���� ����
        transform.LookAt(player);
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    void Flee()
    {
        // ���� ���� ����
        Vector3 fleeDirection = transform.position - player.position;
        fleeDirection.y = 0; // Y�� �̵��� ������� ����
        fleeDirection.Normalize();
        transform.Translate(fleeDirection * Time.deltaTime);
    }

    private void OnMouseEnter()
    {
        isGazeContact = true;
    }

    private void OnMouseExit()
    {
        gazeContactTime = 0.0f;
        isGazeContact = false;
    }
}
