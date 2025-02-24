using UnityEngine;
using System.Collections.Generic;
//interactable behavior for multi-hit targets that moves back to another point on another board when hit, until there are no more future points
public class MultiHitTargetInteractableBehavior : BaseInteractableBehavior
{
    int currentPoint;
    int totalPoints;
    List<GameObject> multiPositions = new List<GameObject>();

    float count;
    int currentBeat;
    Vector3 lastPos;
    Vector3 targetPos;
    bool isMoving;
    public override void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);

        currentPoint = 0;
        currentBeat = 0;
        isMoving = false;
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
            targetPos = multiPositions[currentPoint-1].transform.position;
            transform.position = Vector3.Lerp(lastPos, targetPos, count);
        }
    }

    // Sets the movement target to a target point
    void UpdateMovementTarget()
    {
        lastPos = transform.position;
        targetPos = multiPositions[currentPoint-1].transform.position; // Set the target position to be next point in the list
        transform.parent = multiPositions[currentPoint-1].transform.parent.transform; // Set the parent object to be the new board
        count = 0;
        currentBeat++;
    }

    // Increments the current target point if one still exists, otherwise triggers collision like normal
    public override void AvatarCollision()
    {
        if (!isMoving) // If not moving to another target point
        {
            currentPoint++;
            if (currentPoint - 1 < totalPoints) // If a future point still exists
            {
                isMoving = true;
                // Set target to be the next future point
                UpdateMovementTarget();
                Invoke("StopMoving", 0.1f);
            }
            else
            {
                base.AvatarCollision();
            }
        }
    }

    // Allows the target to be hit again
    void StopMoving()
    {
        isMoving = false;
    }

    // Instantiates the future points as empty game objects and moves them into place
    void SpawnTargetPoints()
    {
        int pointCount = 0; // Keeps track of the total number of target points
        int boardsMovedBack = 0; // Keeps track of the total boards moved back
        foreach (TargetPoints point in interactable.multiPoints)
        {
            pointCount++;
            boardsMovedBack = boardsMovedBack + point.boardsMoved;
            // Create and name the target point as a child of the correct board
            GameObject tmpObject = Instantiate(new GameObject("TargetPoint " + pointCount), LevelManager.Instance.GetSpawnedBoard(boardIndex + boardsMovedBack, stageIndex).transform);
            Quaternion tmpRot = new Quaternion();
            tmpRot.eulerAngles = new Vector3(0, 0, point.interactableAngle);
            tmpObject.transform.localRotation *= tmpRot;
            tmpObject.transform.Translate(Vector3.up * point.interactableDistance);
            tmpObject.transform.localRotation = Quaternion.identity;

            multiPositions.Add(tmpObject);
        }
        totalPoints = multiPositions.Count;
    }
}
