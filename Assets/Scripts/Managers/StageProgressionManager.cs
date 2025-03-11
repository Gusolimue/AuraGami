using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Controls the stage progression visuals and audio
public class StageProgressionManager : MonoBehaviour
{
    public static StageProgressionManager Instance;

    [Header("Variables to Adjust")]
    [SerializeField] float stageProgressionTime;
    [Space]
    [Tooltip("Toggles whether the terrain will change height on a stage transition")] [SerializeField] bool willChangeHeight;
    public float ascendFullDistance; // Determines the height of the environment
    public float ascendCheckDistance; // Height that environment moves to when you fail to beat the stage.
    [Space]
    [Tooltip("Toggles whether the terrain will change speed on a stage transition")] [SerializeField] bool willChangeSpeed;
    [SerializeField] float firstSpeed = 50f;
    [SerializeField] float secondSpeed = 70f;
    [SerializeField] float thirdSpeed = 50f;

    [Header("Variables to Set")]
    public GameObject terrain;
    public int currentStage = 1;
    public float stageLengthTime;

    Vector3 currentHeight;
    Vector3 targetHeight;
    float startTime;
    bool isChangingStage;
    bool isCheckingStage;
    RepeatSegmentedTerrainBehavior repeatSegmentedTerrainBehavior;
    float[] terrainSpeeds = new float[3];
    int speedIndex;
    int targetSpeed;

    public float stageTimer = 0f; // Length of stage 

    void Awake()
    {
        Instance = this;
        speedIndex = 0;
        terrainSpeeds[0] = firstSpeed;
        terrainSpeeds[1] = secondSpeed;
        terrainSpeeds[2] = thirdSpeed;
        repeatSegmentedTerrainBehavior = terrain.GetComponent<RepeatSegmentedTerrainBehavior>();
        repeatSegmentedTerrainBehavior.tempoSpeed = terrainSpeeds[0];
    }

    // Sets the starting positions for the terrain;
    void Start()
    {
        currentHeight = terrain.transform.position;

        StartCoroutine(StageTimer(stageLengthTime));
    }

    // Moves the terrain to the target position over time.
    void Update()
    {
        if (isChangingStage || isCheckingStage)
        {
            float tmpElapsedTime = Time.time - startTime;
            float t = 0f;
            if (isChangingStage) t = Mathf.Clamp(tmpElapsedTime / stageProgressionTime, 0f, 1f);
            else t = Mathf.Clamp(tmpElapsedTime / (stageProgressionTime/1.5f), 0f, 1f);

            if (willChangeHeight) // Change height over the set time
                terrain.transform.position = new Vector3(terrain.transform.position.x, Mathf.Lerp(currentHeight.y, targetHeight.y, t), terrain.transform.position.z);

            if (willChangeSpeed) // Change speed over the set time
                repeatSegmentedTerrainBehavior.tempoSpeed = Mathf.Lerp(terrainSpeeds[speedIndex], terrainSpeeds[targetSpeed], t);

            if (t >= 1) // If stage progression is complete
            {
                if (willChangeHeight) currentHeight = terrain.transform.position;
                if (willChangeSpeed) speedIndex = targetSpeed;
                if (isChangingStage)
                {
                    isChangingStage = false;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_avatar_sigilFill);
                }
                else
                {
                    isCheckingStage = false;
                }
            }
        }
    }

    // Raises and/or lowers the terrain and changes currentStage depending on if the stage was passed
    IEnumerator ProgressStage(bool _success)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_avatar_sigilFill);
        if (_success)
        {
            startTime = Time.time;
            isChangingStage = true;
            if(willChangeHeight) targetHeight = new Vector3(currentHeight.x, currentHeight.y - ascendFullDistance, currentHeight.z);
            if (willChangeSpeed) targetSpeed = speedIndex + 1;

            if (currentStage <= 4) StartCoroutine(StageTimer(stageLengthTime));
        }
        else
        {
            startTime = Time.time;
            isCheckingStage = true;
            if (willChangeHeight) targetHeight = new Vector3(currentHeight.x, currentHeight.y - ascendCheckDistance, currentHeight.z);
            if (willChangeSpeed) targetSpeed = speedIndex + 1;
            yield return new WaitForSeconds(stageProgressionTime);

            startTime = Time.time;
            isCheckingStage = true;
            if (willChangeHeight) targetHeight = new Vector3(currentHeight.x, currentHeight.y + ascendCheckDistance, currentHeight.z);
            if (willChangeSpeed) targetSpeed = speedIndex - 1;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_avatar_sigilFill);
            FadeAllInteractables();

            PauseManager.Instance.isPaused = true;
            if (APManager.Instance.sigils[2].value == APManager.Instance.sigils[2].maxValue)
            {
                CanvasManager.Instance.ShowCanvasLevelEnd();
            }
            else
            {
                CanvasManager.Instance.ShowCanvasStageFail();
            }
        }
    }

    // Counts up to a specified time where it then checks to see if the player can progress to the next stage. 
    public IEnumerator StageTimer(float _stageLengthTime)
    {
        while (stageTimer < _stageLengthTime)
        {
            yield return new WaitForSeconds(1f);
            stageTimer++;
        }
        stageTimer = 0f;
        currentStage++;
        if (APManager.Instance.curAP > currentStage)
        {
            StartCoroutine(ProgressStage(true));
        }
        else
        {
            StartCoroutine(ProgressStage(false));
        }
    }

    // Causes all interactables on all boards to fade out
    private void FadeAllInteractables()
    {
        for (int c = 0; c < 3; c++)
        {
            List<Board> tmpStage = LevelManager.Instance.level.GetStage(c);
            for (int i = 0; i < tmpStage.Count; i++)
            {
                GameObject tmpBoard = LevelManager.Instance.GetSpawnedBoard(i, c);

                BaseInteractableBehavior[] tmpBase = tmpBoard.GetComponentsInChildren<BaseInteractableBehavior>();
                if (tmpBase != null)
                {
                    foreach (BaseInteractableBehavior interactable in tmpBase)
                        interactable.FadeOutTarget();
                }
                HazardInteractableBehavior[] tmpHazard = tmpBoard.GetComponentsInChildren<HazardInteractableBehavior>();
                if (tmpHazard != null)
                {
                    foreach (HazardInteractableBehavior interactable in tmpHazard)
                        interactable.FadeOutTarget();
                }
                MultiHitTargetInteractableBehavior[] tmpMulti = tmpBoard.GetComponentsInChildren<MultiHitTargetInteractableBehavior>();
                if (tmpMulti != null)
                {
                    foreach (MultiHitTargetInteractableBehavior interactable in tmpMulti)
                        interactable.FadeOutTarget();
                }
                PrecisionTargetInteractableBehavior[] tmpPrecision = tmpBoard.GetComponentsInChildren<PrecisionTargetInteractableBehavior>();
                if (tmpPrecision != null)
                {
                    foreach (PrecisionTargetInteractableBehavior interactable in tmpPrecision)
                        interactable.FadeOutTarget();
                }
                TargetInteractableBehavior[] tmpTarget = tmpBoard.GetComponentsInChildren<TargetInteractableBehavior>();
                if (tmpTarget != null)
                {
                    foreach (TargetInteractableBehavior interactable in tmpTarget)
                        interactable.FadeOutTarget();
                }
            }
        }
    }
}
