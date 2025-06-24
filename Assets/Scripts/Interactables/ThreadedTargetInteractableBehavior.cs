using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
public class ThreadedTargetInteractableBehavior : BaseInteractableBehavior
{
    List<GameObject> threadPositions = new List<GameObject>();
    public Material tracingMat;
    public Material failedTraceMat;
    public SplineContainer threadSpline;
    Renderer splineRenderer;
    public BezierKnot[] threadKnots;
    public TargetBaseModelSelect endTargetModelSelect;
    GameObject endTargetObject;
    float count;
    int currentPoint;
    int targetBoardIndex = 0;
    bool isTracing = false;
    [HideInInspector] public bool onSpline = false;
    public override void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);
        endTargetObject = endTargetModelSelect.SelectModel(side);
        endTargetObject.GetComponent<MeshRenderer>().sharedMaterials = interactableObject.GetComponent<MeshRenderer>().sharedMaterials;
        endTargetObject = endTargetModelSelect.gameObject;
        splineRenderer = threadSpline.GetComponent<Renderer>();
        targetBoardIndex = boardIndex;
        //splineExtrude = threadSpline.gameObject.AddComponent<SplineExtrude>();
        //splineExtrude.createMeshInstance = e
        //splineExtrude.Container = threadSpline;
        //splineExtrude.Radius = .05f;
        //splineExtrude.RebuildOnSplineChange = true;
        //threadSpline.gameObject.AddComponent<MeshCollider>();
        //threadSpline.gameObject.GetComponent<MeshCollider>().providesContacts = true;
        currentPoint = 0;
    }

    private void Start()
    {
        // Spawn targets points in Start so all boards have been created
    }

    private void Update()
    {
        //ThreadSplineUpdate();
        count += Time.deltaTime;
    }
    IEnumerator COTrace()
    {
        splineRenderer.sharedMaterial = tracingMat;
        count = 0;
        yield return new WaitUntil(() => onSpline);
        yield return new WaitUntil(()=> !onSpline);

        splineRenderer.sharedMaterial = failedTraceMat;
        isTracing = false;
    }
    public override void InteractableMissed()
    {
        if (currentPoint <= interactable.multiPoints.Length)
        {
            targetBoardIndex += interactable.multiPoints[Mathf.Clamp(currentPoint, 0, interactable.multiPoints.Length - 1)].boardsMoved;
            //Debug.Log("threaded moved");
            this.transform.SetParent(LevelManager.Instance.GetSpawnedBoard(targetBoardIndex, LevelManager.currentStageIndex).gameObject.transform);
            if (currentPoint > interactable.multiPoints.Length)
            {
                isTracing = false;
            }
            currentPoint++;
        }
        else
        {
            //Debug.Log("threadedMissed");
            base.InteractableMissed();
        }
    }

    // Increments the current target point if one still exists, otherwise triggers collision like normal
    public override void AvatarCollision(AvatarBehavior avatarBehavior)
    {
        Debug.Log("ac called " + avatarBehavior);
        if (isTracing)
        {
            Debug.Log("targetComlpete");
            APManager.Instance.IncreaseAP();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_target_hit);
            if (avatarBehavior.side == eSide.left) APVFXManager.Instance.APVfxSpawnNagini(endTargetObject.transform.position, APManager.Instance.multLevels[APManager.Instance.GetStreakIndex()] * 1);
            else APVFXManager.Instance.APVfxSpawnYata(endTargetObject.transform.position, APManager.Instance.multLevels[APManager.Instance.GetStreakIndex()] * 1);
            StopTarget();
        }
        else
        {
            Debug.Log("targetstart");
            isTracing = true;
            StartCoroutine(COTrace());
            interactableRenderer.gameObject.SetActive(false);
        }
    }
     void ThreadSplineUpdate()
    {
        for (int i = 0; i < threadKnots.Length; i++)
        {
            threadKnots[i].Position = this.transform.InverseTransformPoint(threadPositions[i].transform.position);
            threadSpline.Spline.SetKnot(i, threadKnots[i]);
        }
    }
    // Instantiates the future points as empty game objects and moves them into place
    public void SpawnTargetPoints()
    {
        int pointCount = 0; // Keeps track of the total number of target points
        int boardsMovedBack = 0; // Keeps track of the total boards moved back
        threadPositions.Add(this.gameObject);
        foreach (TargetPoints point in interactable.multiPoints)
        {
            pointCount++;
            boardsMovedBack = boardsMovedBack + point.boardsMoved;
            // Create and name the target point as a child of the correct board
            GameObject tmpObject = new GameObject("ThreadPoint " + pointCount);
            tmpObject.transform.SetParent(this.transform.parent);
            tmpObject.transform.localPosition = Vector3.zero;
            tmpObject.transform.Translate(Vector3.forward * (LevelManager.Instance.spawnDistance / LevelManager.beatsToPlayer)* (boardsMovedBack));
            Quaternion tmpRot = new Quaternion();
            tmpRot.eulerAngles = new Vector3(0, 0, point.interactableAngle);
            tmpObject.transform.localRotation *= tmpRot;
            tmpObject.transform.Translate(Vector3.up * point.interactableDistance);
            tmpObject.transform.localRotation = Quaternion.identity;
            Instantiate(Resources.Load("InGame/" + "Interactables/" + "navpoint"), tmpObject.transform);
            threadPositions.Add(tmpObject);
        }
        threadKnots = new BezierKnot[threadPositions.Count];
        foreach (var knot in threadKnots)
        {
            threadSpline.Spline.Add(knot, TangentMode.AutoSmooth);
        }
        //for (int i = 0; i < threadPositions.Count; i++)
        //{
        //    threadKnots.Add(new BezierKnot());
        //    threadSpline.Spline.Add(threadKnots[i]);
        //}
        //threadLine.enabled = true;
        //threadLine.positionCount = threadPositions.Count;
        endTargetObject.gameObject.transform.SetParent(threadPositions[threadPositions.Count-1].transform);
        endTargetObject.gameObject.transform.localPosition = Vector3.zero;
        ThreadSplineUpdate();
    }
}
