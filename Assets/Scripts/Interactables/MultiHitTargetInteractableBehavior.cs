using UnityEngine;
using System.Collections.Generic;
//interactable behavior for multi-hit targets that moves back to another point on another board when hit, until there are no more future points
public class MultiHitTargetInteractableBehavior : BaseInteractableBehavior
{
    int currentPoint;
    int totalPoints;
    List<GameObject> multiPositions = new List<GameObject>();

    float count;
    float originSpawnDistance;
    public int currentBeat;
    Vector3 originPos;
    Vector3 lastPos;
    public Vector3 targetPos;
    public override void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);

        currentPoint = -1;
        currentBeat = 0;
    }

    private void Start()
    {
        // Spawn targets in Start so all boards have been created
        SpawnTargetPoints();
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (currentBeat > 0)
        {
            targetPos = multiPositions[currentPoint].transform.position;
            transform.position = Vector3.Lerp(lastPos, targetPos, /*60f / LevelManager.Instance.level.soTrack.bpm * */count);
        }
    }

    // Sets the movement target to a target point
    void UpdateMovementTarget()
    {
        lastPos = transform.position;
        targetPos = multiPositions[currentPoint].transform.position; // Set the target position to be next point in the list
        count = 0;
        currentBeat++;
        Debug.Log("Movement Updated!");
    }

    // Increments the current target point if one still exists, otherwise triggers collision like normal
    public override void AvatarCollision()
    {
        currentPoint++;
        if (currentPoint+1 < totalPoints) // If a future point still exists
        {
            // Set target to be the next future point
            UpdateMovementTarget();
        }
        else
        {
            //BeatManager.beatUpdated -= UpdateMovementTarget;
            base.AvatarCollision();
        }
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
