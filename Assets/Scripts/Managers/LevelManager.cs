using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Attributes"), Space]
    public float spawnDistance;
    public int beatsToPlayer;

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
            container.transform.Translate(Vector3.forward * spawnDistance);
        }
    }

    // Returns the board GameObject at the given index and stage
    public GameObject GetSpawnedBoard(int _boardIndex, int _stageIndex)
    {
        return instantiatedStages[_stageIndex][_boardIndex];
    }
    public void InitLevel()
    {
        //levelContainer = new GameObject("Level Container");
        //levelContainer.transform.parent = this.transform;
        //int tmp = 0;
        //stage1Container = new GameObject("Stage 1 Preview");
        //stage1Container.transform.parent = levelContainer.transform;
        //stage2Container = new GameObject("Stage 2 Preview");
        //stage2Container.transform.parent = levelContainer.transform;
        //stage3Container = new GameObject("Stage 3 Preview");
        //stage3Container.transform.parent = levelContainer.transform;

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


        //for (int i = 0; i < level.GetStage(1).Count; i++)
        //{
        //    level.SpawnBoard(i, level.GetStage(1), stage1Container.transform);
        //    instantiatedStages.Add(level.currentBoard);
        //    instantiatedStages[i].SetActive(false);
        //    tmp = i;
        //}
        //for (int i = 0; i < level.GetStage(2).Count; i++)
        //{
        //    level.SpawnBoard(i, level.GetStage(2), stage2Container.transform);
        //    instantiatedStages.Add(level.currentBoard);
        //    instantiatedStages[i + level.GetStage(1).Count - 1].SetActive(false);

        //}

        //for (int i = 0; i < level.GetStage(3).Count; i++)
        //{
        //    level.SpawnBoard(i, level.GetStage(3), stage3Container.transform);
        //    instantiatedStages.Add(level.currentBoard);
        //    instantiatedStages[i + level.GetStage(1).Count - 1 + level.GetStage(2).Count].SetActive(false);

        //}
        BeatManager.beatUpdated += ActivateBoard;
        isSubscribed = true;
        APManager.Instance.SetTargetValues();
    }
    public void NextStage()
    {
        currentStageIndex += 1;
        if (APManager.Instance.StagePassCheck())
        {
            CanvasManager.Instance.ShowCanvasStageFail();
            PauseManager.Instance.isPaused = true;
        }
        else if (currentStageIndex >= 2)
        {
            CanvasManager.Instance.ShowCanvasLevelEnd();
            PauseManager.Instance.isPaused = true;
        }
        else
        {
            boardCount = 0;

            APManager.Instance.ResetAP();
        }
    }
    void ActivateBoard()
    {
        if(boardCount >= instantiatedStages[currentStageIndex].Count)
        {
            Debug.Log("stage finished");
            if(currentStageIndex >= 2)
            {
                BeatManager.beatUpdated -= ActivateBoard;
                isSubscribed = false;
            }
            else
            {
                NextStage();
            }
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
