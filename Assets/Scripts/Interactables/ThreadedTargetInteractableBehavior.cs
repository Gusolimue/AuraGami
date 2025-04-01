using System.Collections.Generic;
using UnityEngine;

public class ThreadedTargetInteractableBehavior : BaseInteractableBehavior
{
    int currentPoint;
    int totalPoints;
    List<GameObject> threadPositions = new List<GameObject>();

    public Renderer endTargetRenderer;
    LineRenderer threadLine;
    float count;
    int currentBeat;
    Vector3 lastPos;
    Vector3 targetPos;
    bool isTracing = false;
    public override void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);
        threadLine = GetComponent<LineRenderer>();
        threadLine.enabled = false;
        endTargetRenderer.sharedMaterial = interactableRenderer.sharedMaterial;
        currentPoint = 0;
        currentBeat = 0;
    }

    private void Start()
    {
        // Spawn targets points in Start so all boards have been created
        SpawnTargetPoints();
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (currentBeat > 0)
        {
            targetPos = threadPositions[currentPoint - 1].transform.position;
            transform.position = Vector3.Lerp(lastPos, targetPos, count);
        }
    }
    private void LateUpdate()
    {
        ThreadLineUpdate();
    }

    // Increments the current target point if one still exists, otherwise triggers collision like normal
    public override void AvatarCollision()
    {
        if(isTracing)
        {
            APManager.Instance.IncreaseAP();
            base.AvatarCollision();
        }
        else
        {
            isTracing = true;
        }
    }
     void ThreadLineUpdate()
    {
        for (int i = 0; i < threadLine.positionCount; i++)
        {
            threadLine.SetPosition(i, threadPositions[i].transform.position);
        }
    }
    // Instantiates the future points as empty game objects and moves them into place
    void SpawnTargetPoints()
    {
        int pointCount = 0; // Keeps track of the total number of target points
        int boardsMovedBack = 0; // Keeps track of the total boards moved back
        threadPositions.Add(this.gameObject);
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
        endTargetRenderer.gameObject.transform.SetParent(threadPositions[threadPositions.Count-1].transform);
        endTargetRenderer.gameObject.transform.position = Vector3.zero;
    }
}
