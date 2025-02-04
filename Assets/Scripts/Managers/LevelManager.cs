using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level level;
    public static LevelManager Instance;
    [Header("Level Attributes"), Space]
    public float spawnDistance;
    public int beatsToPlayer;

    private void Awake()
    {
        Instance = this;
    }
    public void InitLevel()
    {

    }
    // set the spawn distance. set number of beats for travel time
    // targets will interpolate forwards (spawn distance/number of beats) * beats passed since spawn + 1 over the course of 1 beat
    //
}
