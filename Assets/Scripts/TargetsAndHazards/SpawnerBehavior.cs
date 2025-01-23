using UnityEngine;
public enum eTargetType {none, left, right, obstacle }
public enum eTargetPositions {topLeft, topCenter, topRight, bottomLeft, bottomCenter, bottomRight,
    middleUpperRight, middleUpperLeft, middleBottomLeft, middleBottomRight }
[System.Serializable]
public class Level
{
    public Board[] boards;
}

[System.Serializable]
public class Board
{
    public eTargetType topLeftTarget;
    public eTargetType bottomLeftTarget;
    public eTargetType topRightTarget;
    public eTargetType bottomRightTarget;
}
public class SpawnerBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public Level level;
    [Header("Variables to Set")]
    public GameObject leftTargetPrefab;
    public GameObject rightTargetPrefab;
    public GameObject obstaclePrefab;
    public GameObject boardPrefab;
    [Header("Variables to Call")]
    public GameObject[] targetsAndObstacles;
    public Transform[] spawnPoints;
    public float beatTime = (60/130)*2;
    int boardCount = 0;
    void Update()
    {

    }
    public void SpawnBoard()
    {
        // Instantiate board (write exception to stop when index is out of range of array)
        // for each variable in board script...
        SpawnTarget(level.boards[boardCount].topLeftTarget, null);
        SpawnTarget(level.boards[boardCount].bottomLeftTarget, null);
        SpawnTarget(level.boards[boardCount].topRightTarget, null);
        SpawnTarget(level.boards[boardCount].bottomRightTarget, null);

        boardCount++;
    }

    public void SpawnTarget(eTargetType _targetType, Transform _spawnPosition)
    {

        switch (_targetType)
        {
            case eTargetType.left:
                //instantiate correct prefab at _spawnPosition
                break;
            case eTargetType.right:
                //instantiate correct prefab at _spawnPosition
                break;
            case eTargetType.obstacle:
                //instantiate correct prefab at _spawnPosition
                break;
            default:
                break;
        }

        //script that 
    }
    public void GetSpawnPosition()
    {

    }
}
