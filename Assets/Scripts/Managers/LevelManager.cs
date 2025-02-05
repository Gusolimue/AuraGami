using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level level;
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
    }
    public void InitLevel()
    {
        for (int i = 0; i < level.GetStage(1).Count; i++)
        {
            level.SpawnBoard(i, this.transform);
            instantiatedLevel.Add(level.currentBoard);
            instantiatedLevel[i].SetActive(false);

        }
    }

    // set the spawn distance. set number of beats for travel time
    // targets will interpolate forwards (spawn distance/number of beats) * beats passed since spawn + 1 over the course of 1 beat
    //
}
