using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level level;
    GameObject levelContainer;
    public static LevelManager Instance;
    public List<GameObject> instantiatedLevel;
    [Header("Level Attributes"), Space]
    public float spawnDistance;
    public int beatsToPlayer;

    GameObject stage1Container;
    GameObject stage2Container;
    GameObject stage3Container;
    int boardCount;
    private void Awake()
    {
        Instance = this;
        InitLevel();
        levelContainer.transform.Translate(Vector3.forward * spawnDistance);
    }

    // Returns the board GameObject at the given index and stage
    public GameObject GetSpawnedBoard(int _boardIndex, int _stageIndex)
    {
        //returns the instantiated board when given index
        //if stage 1, board index is fine. if stage 2 or 3, you need to add instantiatedLevel.count()/3 or (instantiatedLevel.Count()/3 * 2) respectively
        if (_stageIndex == 2)
            _boardIndex = _boardIndex + instantiatedLevel.Count / 3;
        else if (_stageIndex == 3)
            _boardIndex = _boardIndex + instantiatedLevel.Count / 3 * 2;
        return instantiatedLevel[_boardIndex];
    }
    public void InitLevel()
    {
        levelContainer = new GameObject("Level Container");
        levelContainer.transform.parent = this.transform;
        int tmp = 0;
        stage1Container = new GameObject("Stage 1 Preview");
        stage1Container.transform.parent = levelContainer.transform;
        stage2Container = new GameObject("Stage 2 Preview");
        stage2Container.transform.parent = levelContainer.transform;
        stage3Container = new GameObject("Stage 3 Preview");
        stage3Container.transform.parent = levelContainer.transform;
        for (int i = 0; i < level.GetStage(1).Count; i++)
        {
            level.SpawnBoard(i, level.GetStage(1), stage1Container.transform);
            instantiatedLevel.Add(level.currentBoard);
            instantiatedLevel[i].SetActive(false);
            tmp = i;
        }
        for (int i = 0; i < level.GetStage(2).Count; i++)
        {
            level.SpawnBoard(i, level.GetStage(2), stage2Container.transform);
            instantiatedLevel.Add(level.currentBoard);
            instantiatedLevel[i + level.GetStage(1).Count - 1].SetActive(false);

        }

        for (int i = 0; i < level.GetStage(3).Count; i++)
        {
            level.SpawnBoard(i, level.GetStage(2), stage3Container.transform);
            instantiatedLevel.Add(level.currentBoard);
            instantiatedLevel[i + level.GetStage(1).Count - 1 + level.GetStage(2).Count].SetActive(false);

        }
        BeatManager.beatUpdated += ActivateBoard;
    }

    void ActivateBoard()
    {
        if(boardCount > instantiatedLevel.Count)
        {
            BeatManager.beatUpdated -= ActivateBoard;
        }
        else
        {          
            instantiatedLevel[boardCount].SetActive(true);
            instantiatedLevel[boardCount].GetComponent<BoardBehavior>().StartMovement();
            boardCount++;
        }
    }
    // set the spawn distance. set number of beats for travel time
    // targets will interpolate forwards (spawn distance/number of beats) * beats passed since spawn + 1 over the course of 1 beat
    //
}
