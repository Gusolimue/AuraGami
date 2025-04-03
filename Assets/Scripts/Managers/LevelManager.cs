using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Attributes"), Space]
    public float spawnDistance;
    public static int beatsToPlayer = 8;

    [Header("Attributes To Set/Call"), Space]
    public Level level;
    public static LevelManager Instance;

    [Header("InGame Attributes"), Space]
    public static int currentStageIndex;
    public List<GameObject>[] instantiatedStages;

    GameObject[] stageContainers;
    GameObject stage1Container;
    GameObject stage2Container;
    GameObject stage3Container;
    int boardCount;

    bool isSubscribed;
    private void Awake()
    {
        Instance = this;
        currentStageIndex = 0;
    }
    private void Start()
    {
        InitLevel();
        foreach (var container in stageContainers)
        {
            container.transform.position = AvatarManager.Instance.avatarCircTransform.position;
            container.transform.Translate(Vector3.forward * spawnDistance);
        }
    }

    // Returns the board GameObject at the given index and stage
    public GameObject GetSpawnedBoard(int _boardIndex, int _stageIndex)
    {
        Debug.Log("stage index" + _stageIndex);
        Debug.Log("board index" + _boardIndex);
        return instantiatedStages[_stageIndex][_boardIndex];
    }
    public void InitLevel()
    {
        stageContainers = new GameObject[3];
        instantiatedStages = new List<GameObject>[3];

        for (int i = 0; i < 3; i++)
        {
            stageContainers[i] = new GameObject("Stage " + (i + 1) + " Preview");
            stageContainers[i].transform.parent = this.transform;
            instantiatedStages[i] = new List<GameObject>();
        }

        for (int i = 0; i < 3; i++)
        {
            for (int c = 0; c < level.GetStage(i+1).Count; c++)
            {
                level.SpawnBoard(c, level.GetStage(i+1), stageContainers[i].transform);
                instantiatedStages[i].Add(level.currentBoard);
                instantiatedStages[i][c].SetActive(false);

            }
        }
        StartStage();
        APManager.Instance.SetTargetValues();
    }
    public void NextStage()
    {
        if (!APManager.Instance.StagePassCheck())
        {
            CanvasManager.Instance.ShowCanvasStageFail();
            PauseManager.Instance.PauseGame(true);
        }
        else if (currentStageIndex >= 2)
        {
            Debug.Log("end level");
            CanvasManager.Instance.ShowCanvasLevelEnd();
            PauseManager.Instance.PauseGame(true);
        }
        else
        {
            boardCount = 0;

            APManager.Instance.ResetAP();
        }

        currentStageIndex += 1;
    }
    public void StartStage()
    {
        Debug.Log("stage started: " + (currentStageIndex+1));
        boardCount = 0;
        BeatManager.beatUpdated += ActivateBoard;
        isSubscribed = true;
    }
    public void EndStage()
    {
        Debug.Log("stage finished :" + (currentStageIndex + 1));
        BeatManager.beatUpdated -= ActivateBoard;
        isSubscribed = false;
        currentStageIndex += 1;
    }
    void ActivateBoard()
    {
        //Debug.Log("board activated");
        if(boardCount >= instantiatedStages[currentStageIndex].Count)
        {
            EndStage();
            //if (currentStageIndex >= 2)
            //{
            //    BeatManager.beatUpdated -= ActivateBoard;
            //    isSubscribed = false;
            //    NextStage();
            //}
            //else
            //{
            //    NextStage();
            //}
        }
        else
        {          
            instantiatedStages[currentStageIndex][boardCount].SetActive(true);
            instantiatedStages[currentStageIndex][boardCount].GetComponent<BoardBehavior>().StartMovement();
            boardCount++;
        }
    }

    private void OnDestroy()
    {
        if(isSubscribed)
        {
            BeatManager.beatUpdated -= ActivateBoard;
            isSubscribed = false;
        }
    }
    // set the spawn distance. set number of beats for travel time
    // targets will interpolate forwards (spawn distance/number of beats) * beats passed since spawn + 1 over the course of 1 beat
    //
}
