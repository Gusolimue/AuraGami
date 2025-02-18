using UnityEngine;
using System.Collections.Generic;
//interactable behavior for multi-hit targets that moves back to another point on another board when hit, until there are no more future points
public class MultiHitTargetInteractableBehavior : BaseInteractableBehavior
{
    // TODO:
    // When hit, set the next empty in the array as the target position to move to. (test)
    // Lerps to position of next point when hit. (test)
    // Stops target as normal when there are not more future points. (test)
    // Set the target points to be on the right boards

    int currentPoint;
    int totalPoints;
    List<GameObject> multiPositions = new List<GameObject>();

    float count;
    float originSpawnDistance;
    public int currentBeat;
    Vector3 originPos;
    Vector3 lastPos;
    public Vector3 targetPos;
    public override void InitInteractable(eSide _eSide, int _stage, int _board, int _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);

        currentPoint = 0;
        SpawnTargetPoints();
        totalPoints = multiPositions.Count;
    }

    private void Update()
    {
        //look in board behavior for lerping example
    }

    // Sets the movement target to a target point
    public void UpdateMovementTarget()
    {
        //base.UpdateMovementTarget();
        targetPos = multiPositions[currentPoint].transform.position; // Set the target position to be next point in the list
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
            base.AvatarCollision();
        }
    }

    // Instantiates the future points as empty game objects and moves them into place
    void SpawnTargetPoints()
    {
        int pointCount = 0; // Keeps track of the total number of points
        foreach (TargetPoints point in interactable.multiPoints)
        {
            pointCount++;
            GameObject tmpObject = new GameObject("TargetPoint of " + pointCount); // Creates and names the point
            // tmpObject.transform = uses the get methods in level manager to set parent as the correct board
            Quaternion tmpRot = new Quaternion();
            tmpRot.eulerAngles = new Vector3(0, 0, point.interactableAngle);
            tmpObject.transform.localRotation *= tmpRot;
            tmpObject.transform.Translate(Vector3.up * point.interactableDistance);
            tmpObject.transform.localRotation = Quaternion.identity;

            multiPositions.Add(tmpObject);
        }
    }
}
