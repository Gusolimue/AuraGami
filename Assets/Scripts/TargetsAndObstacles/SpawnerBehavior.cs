using UnityEngine;
public enum eTargetType {none, left, right, obstacle }
[System.Serializable]
public class Board
{
    public Target[] targets;
    public int waitTime;
}
[System.Serializable]
public class Target
{
    public eTargetType targetType;
    public eTargetPositions targetPosition;
}
public class SpawnerBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public SoLevel level;
    [Header("Variables to Set")]
    public GameObject leftTargetPrefab;
    public GameObject rightTargetPrefab;
    public GameObject obstaclePrefab;
    public GameObject boardPrefab;

    BoardBehavior currentBoard;
    int boardCount = 0;
    int waitCount = 0;
    //[Header("Variables to Call")]
    void Update()
    {

    }
    public void SpawnBoard()
    {
        // Instantiate board (write exception to stop when index is out of range of array)
        // for each variable in board script...
        if (boardCount < level.boards.Length && waitCount >= level.boards[boardCount].waitTime)
        {
            currentBoard = Instantiate(boardPrefab, transform.position, transform.rotation).GetComponent<BoardBehavior>();
            foreach (var Target in level.boards[boardCount].targets)
            {
                SpawnTarget(Target);
            }
            waitCount = 0;
            boardCount++;
        }
        waitCount++;
    }

    public void SpawnTarget(Target _target)
    {
        float tmpRadius = /*CircleManager.Instance.avatarCircDiameter*/ 1.85f / 2f;

        GameObject tmpObject;
        switch (_target.targetType)
        {
            case eTargetType.left:
                tmpObject = Instantiate(leftTargetPrefab, currentBoard.spawnPositions[(int)_target.targetPosition].transform);
                break;
            case eTargetType.right:
                tmpObject = Instantiate(rightTargetPrefab, currentBoard.spawnPositions[(int)_target.targetPosition].transform);
                break;
            case eTargetType.obstacle:
                tmpObject = Instantiate(obstaclePrefab, currentBoard.spawnPositions[(int)_target.targetPosition].transform);
                break;
            default:
                break;
        }
    }
}
