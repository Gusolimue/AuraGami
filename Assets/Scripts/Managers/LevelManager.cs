using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Attributes"), Space]
    public float spawnDistance;
    public static int beatsToPlayer = 8;

    [Header("Attributes To Set/Call"), Space]
    //public Level level;
    public SoLevel level;
    public static LevelManager Instance;
    public GameObject[] threadedTargets;

    [Header("InGame Attributes"), Space]
    public static int currentStageIndex;
    public List<GameObject>[] instantiatedStages;
    public int threadedCount = 0;

    GameObject[] stageContainers;

    GameObject[] trackPoints;
    int boardCount;
    GameObject currentBoard;
    bool isSubscribed;
    private void Awake()
    {
        Instance = this;
        currentStageIndex = 0;
    }
    private void Start()
    {
        InitLevel();
        StartStage();
    }

    public void SpawnBoard(int num, List<Board> _stage, Transform transform, int _stageIndex = 0)
    {
        currentBoard = Instantiate(Resources.Load("InGame/" + "Interactables/" + "BoardPrefab")
            as GameObject, transform);
        foreach (var Target in _stage[num].interactables)
        {
            SpawnTarget(Target, _stageIndex);
        }
        if (_stage[num].interactables.Length == 0)
        {
            currentBoard.name = "Empty Board";
        }
        boardCount += 1;
    }

    //called to spawn every individual target when a board is spawned
    void SpawnTarget(Interactable _target, int _index)
    {
        GameObject tmpObject;
        switch (_target.interactableType)
        {
            case eTargetType.regularTarget:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "regularTargetPrefab")
            as GameObject, currentBoard.transform);
                break;
            case eTargetType.multihitTarget:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "multihitTargetPrefab")
            as GameObject, currentBoard.transform);
                break;
            case eTargetType.threadedTarget:
                //tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "threadedTargetPrefab")as GameObject, currentBoard.transform);
                tmpObject = threadedTargets[threadedCount];
                threadedCount++;
                tmpObject.transform.SetParent(currentBoard.transform);
                tmpObject.transform.localPosition = Vector3.zero;
                break;
            case eTargetType.precisionTarget:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "precisionTargetPrefab")
            as GameObject, currentBoard.transform);
                break;
            case eTargetType.regularObstacle:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "regularObstaclePrefab")
            as GameObject, currentBoard.transform);
                break;
            default:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "regularTargetPrefab")
            as GameObject, currentBoard.transform);
                break;
        }
        tmpObject.GetComponent<BaseInteractableBehavior>().InitInteractable(_target.side, _index, boardCount, _target);
        Quaternion tmpRot = new Quaternion();
        tmpRot.eulerAngles = new Vector3(0, 0, _target.interactableAngle);
        tmpObject.transform.localRotation *= tmpRot;
        tmpObject.transform.Translate(Vector3.up * _target.interactableDistance);
        tmpObject.transform.localRotation = Quaternion.identity;
    }
    // Returns the board GameObject at the given index and stage
    public GameObject GetSpawnedBoard(int _boardIndex, int _stageIndex)
    {
        //Debug.Log("stage index" + _stageIndex);
        //Debug.Log("board index" + _boardIndex);
        return instantiatedStages[_stageIndex][_boardIndex];
    }

    public Transform GetTrackPointTransform(int _index)
    {
        return trackPoints[Mathf.Clamp(_index, 0, trackPoints.Length-1)].transform;
    }
    public void InitLevel()
    {
        stageContainers = new GameObject[3];
        instantiatedStages = new List<GameObject>[3];
        trackPoints = new GameObject[beatsToPlayer + 2];
        GameObject trackContainer = new GameObject("Track Container");
        trackContainer.transform.SetParent(this.transform);
        trackContainer.transform.position = AvatarManager.Instance.avatarCircTransform.position;
        for (int i = 0; i < beatsToPlayer + 2; i++)
        {
            trackPoints[i] = new GameObject("[" + i + "] Track Point");
            trackPoints[i].transform.SetParent(trackContainer.transform);
            trackPoints[i].transform.localPosition = Vector3.zero;
            trackPoints[i].transform.Translate(Vector3.forward * spawnDistance);
            trackPoints[i].transform.Translate(Vector3.back * spawnDistance/ beatsToPlayer * i);
        }

        for (int i = 0; i < 3; i++)
        {
            stageContainers[i] = new GameObject("Stage " + (i + 1) + " Boards");
            stageContainers[i].transform.parent = this.transform;
            instantiatedStages[i] = new List<GameObject>();
        }

        for (int i = 0; i < 3; i++)
        {
            for (int c = 0; c < level.GetStage(i + 1).Count; c++)
            {
                SpawnBoard(c, level.GetStage(i + 1), stageContainers[i].transform, i);
                instantiatedStages[i].Add(currentBoard);
                instantiatedStages[i][c].SetActive(false);

            }
        }
        foreach (var stage in instantiatedStages)
        {
            foreach (var board in stage)
            {
                Debug.Log("spawning target points");
                foreach (var behavior in board.GetComponentsInChildren<MultiHitTargetInteractableBehavior>())
                {
                    behavior.SpawnTargetPoints();
                }
            }
        }

        foreach (var container in stageContainers)
        {
            container.transform.position = AvatarManager.Instance.avatarCircTransform.position;
            container.transform.Translate(Vector3.forward * spawnDistance);
        }
        APManager.Instance.SetTargetValues();
    }
    public void NextStage()
    {
        if (!APManager.Instance.StagePassCheck())
        {
            CanvasManager.Instance.ShowCanvasStageFail();
            PauseManager.Instance.isPaused = true;
            BeatManager.Instance.PauseMusicTMP(true);
            //PauseManager.Instance.PauseGame(true);
        }
        else if (currentStageIndex >= 2)
        {
            Debug.Log("end level");
            CanvasManager.Instance.ShowCanvasLevelEnd();
            PauseManager.Instance.isPaused = true;
            //PauseManager.Instance.PauseGame(true);
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
            instantiatedStages[currentStageIndex][boardCount].GetComponent<BoardBehavior>().Init();
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
