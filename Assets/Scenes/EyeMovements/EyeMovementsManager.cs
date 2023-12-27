using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeMovementsManager : MonoBehaviour
{
    private enum EyeMovement
    {
        None,
        Fixation,
        SmoothPursuit,
        Saccade
    }
    [SerializeField]
    private EyeMovement eyeMovement;

    [Header("Tutorial Setting")]
    public Text eyeMovementText;
    public GameObject fixationTarget;
    public GameObject saccadeTarget;
    public GameObject smoothPursuitTarget;

    public int targetsRemaining = 5;
    private int total = 5;

    private float minX = -6f;
    private float maxX = 6f;
    private float minY = 2f;
    private float maxY = 7f;

    private AudioSource audioSource;
    public GameObject destroySounder;

    // Start is called before the first frame update
    void Start()
    {
        eyeMovement = EyeMovement.None;
        eyeMovementText.text = "Eye Movement";
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayFixation()
    {
        audioSource.Play();

        targetsRemaining = total;
        eyeMovement = EyeMovement.Fixation;
        eyeMovementText.text = "Fixation";

        for (int i = 0; i < total; i++)
            SpawnTarget();
    }

    public void PlaySaccade()
    {
        audioSource.Play();

        targetsRemaining = total;
        eyeMovement = EyeMovement.Saccade;
        eyeMovementText.text = "Saccade";

        SpawnTarget();
    }

    public void PlaySmoothPusuit()
    {
        audioSource.Play();

        targetsRemaining = total;
        eyeMovement = EyeMovement.SmoothPursuit;
        eyeMovementText.text = "SmoothPursuit";

        SpawnTarget();
    }

    public void SpawnTarget()
    {
        // Spawn a new target point at a random position
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        Vector3 spawnPosition = randomPosition; //transform.position + 

        GameObject targetType;
        switch (eyeMovement)
        {
            case EyeMovement.Fixation: targetType = fixationTarget; break;
            case EyeMovement.SmoothPursuit: targetType = smoothPursuitTarget; break;
            case EyeMovement.Saccade: targetType = saccadeTarget; break;
            default: targetType = null; break;
        }

        //targets[index] = Instantiate(targetPointPrefab, transform.position + randomPosition, Quaternion.identity);
        GameObject target = Instantiate(targetType, spawnPosition, Quaternion.identity);
        EyeMovementsTarget targetScript = target.GetComponent<EyeMovementsTarget>();

        targetScript.manager = this;
    }

    public void TargetDestroyed()
    {
        destroySounder.GetComponent<AudioSource>().Play();

        if (targetsRemaining <= 0)
        {
            GameClear();
        }
    }

    void GameClear()
    {
        audioSource.Play();
        eyeMovementText.text = "Clear";
    }
}
