using UnityEngine;
//spawns boards from the level variable through a method called by beatmanager
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

    //spawns a board with its targets when called
    public void SpawnBoard()
    {
        //if (boardCount < level.boards.Length && waitCount >= level.boards[boardCount].waitTime)
        //{
        //    currentBoard = Instantiate(boardPrefab, transform.position, transform.rotation).GetComponent<BoardBehavior>();
        //    foreach (var Target in level.boards[boardCount].interactables)
        //    {
        //        SpawnTarget(Target);
        //    }
        //    waitCount = 0;
        //    boardCount++;
        //}
        //waitCount++;
    }
    //called to spawn every individual target when a board is spawned
    public void SpawnTarget(Interactable _target)
    {
        GameObject tmpObject;
        //switch (_target.interactableType)
        //{
        //    case eTargetType.left:
        //        tmpObject = Instantiate(leftTargetPrefab, currentBoard.spawnPositions[(int)_target.targetPosition].transform);
        //        break;
        //    case eTargetType.right:
        //        tmpObject = Instantiate(rightTargetPrefab, currentBoard.spawnPositions[(int)_target.targetPosition].transform);
        //        break;
        //    case eTargetType.obstacle:
        //        tmpObject = Instantiate(obstaclePrefab, currentBoard.spawnPositions[(int)_target.targetPosition].transform);
        //        break;
        //    default:
        //        break;
        //}
    }
}
