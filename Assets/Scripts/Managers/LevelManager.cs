using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level level;
    public GameObject levelContainer;
    public static LevelManager Instance;
    public List<GameObject> instantiatedLevel;
    [Header("Level Attributes"), Space]
    public float spawnDistance;
    public int beatsToPlayer;

    int boardCount;
    private void Awake()
    {
        Instance = this;
        InitLevel();
        levelContainer.transform.Translate(Vector3.forward * spawnDistance);
    }
    public void InitLevel()
    {
        for (int i = 0; i < level.GetStage(1).Count; i++)
        {
            level.SpawnBoard(i, levelContainer.transform);
            instantiatedLevel.Add(level.currentBoard);
            instantiatedLevel[i].SetActive(false);

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
            boardCount++;
        }
    }
    // set the spawn distance. set number of beats for travel time
    // targets will interpolate forwards (spawn distance/number of beats) * beats passed since spawn + 1 over the course of 1 beat
    //
}
