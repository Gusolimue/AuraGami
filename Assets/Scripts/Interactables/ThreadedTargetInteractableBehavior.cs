using System.Collections.Generic;
using UnityEngine;

public class ThreadedTargetInteractableBehavior : BaseInteractableBehavior
{
    int currentPoint;
    int totalPoints;
    public List<GameObject> threadPositions = new List<GameObject>();

    LineRenderer threadLine;
    float count;
    int currentBeat;
    Vector3 lastPos;
    Vector3 targetPoint;
    public bool isTracing = false;
    int targetBoardIndex;
    public override void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);
        threadLine = GetComponent<LineRenderer>();
        threadLine.enabled = false;
        currentPoint = 0;
        currentBeat = 0;
        targetBoardIndex = boardIndex;
    }

    private void Start()
    {
        // Spawn targets points in Start so all boards have been created
        SpawnTargetPoints();
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isTracing)
        {
            float tmpTime = count / 60f / LevelManager.Instance.level.soTrack.bpm / interactable.multiPoints[Mathf.Clamp(currentPoint, 0, interactable.multiPoints.Length-1)].boardsMoved;

            //tmpTime =
            transform.position = Vector3.Lerp(threadPositions[currentPoint].transform.position, threadPositions[currentPoint + 1].transform.position, count * 3);
            if (currentPoint >= threadPositions.Count) isTracing = false;
        }
    }
    private void LateUpdate()
    {
        ThreadLineUpdate();
    }

    List<int> oldPoints = new List<int>(0);
    public override void InteractableMissed()
    {
        if (isTracing)
        {
            Debug.Log("threaded moved");
            targetBoardIndex += interactable.multiPoints[Mathf.Clamp(currentPoint, 0, interactable.multiPoints.Length-1)].boardsMoved;
            this.transform.SetParent(LevelManager.Instance.GetSpawnedBoard(targetBoardIndex, LevelManager.currentStageIndex).gameObject.transform);
            threadPositions[currentPoint].transform.SetParent(LevelManager.Instance.GetSpawnedBoard(targetBoardIndex, LevelManager.currentStageIndex).gameObject.transform);
            currentPoint++;
            count = 0;
        }
        else
        {
            Debug.Log("threadedMissed");
            base.InteractableMissed();
        }
        if (currentPoint >= threadPositions.Count - 1) isTracing = false;
    }

    // Increments the current target point if one still exists, otherwise triggers collision like normal
    public override void AvatarCollision()
    {
        isTracing = true;
        count = 0;
        //if (isTracing)
        //{
        //    APManager.Instance.IncreaseAP();
        //    base.AvatarCollision();
        //}
        //else
        //{
        //    isTracing = true;
        //}
    }

     void ThreadLineUpdate()
    {
        //bool tmpSet = true;
        //int tmpindex = 0;
        for (int i = 0; i < threadLine.positionCount; i++)
        {
            threadLine.SetPosition(i, threadPositions[i].transform.position);
            //if (threadPositions[i].activeInHierarchy && tmpSet)
            //{
            //    tmpSet = false;
            //}
        }

        //foreach (var item in oldPoints)
        //{
        //    threadLine.SetPosition(item, threadPositions[tmpindex].transform.position);
        //}
    }
    // Instantiates the future points as empty game objects and moves them into place
    void SpawnTargetPoints()
    {
        int pointCount = 0; // Keeps track of the total number of target points
        int boardsMovedBack = 0; // Keeps track of the total boards moved back
        threadPositions.Add(new GameObject("Start Thread Position"));
        threadPositions[0].transform.SetParent(LevelManager.Instance.GetSpawnedBoard(boardIndex, stageIndex).transform);
        threadPositions[0].transform.position = this.gameObject.transform.position;
        foreach (TargetPoints point in interactable.multiPoints)
        {
            pointCount++;
            boardsMovedBack = boardsMovedBack + point.boardsMoved;
            // Create and name the target point as a child of the correct board
            GameObject tmpObject = Instantiate(new GameObject("ThreadPoint " + pointCount), LevelManager.Instance.GetSpawnedBoard(boardIndex + boardsMovedBack, stageIndex).transform);
            Quaternion tmpRot = new Quaternion();
            tmpRot.eulerAngles = new Vector3(0, 0, point.interactableAngle);
            tmpObject.transform.localRotation *= tmpRot;
            tmpObject.transform.Translate(Vector3.up * point.interactableDistance);
            tmpObject.transform.localRotation = Quaternion.identity;

            threadPositions.Add(tmpObject);
        }
        threadLine.enabled = true;
        threadLine.positionCount = threadPositions.Count;
        totalPoints = threadPositions.Count;
    }
}
