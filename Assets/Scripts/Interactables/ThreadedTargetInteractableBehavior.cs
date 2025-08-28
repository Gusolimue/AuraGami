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
    [HideInInspector] public float splineCount;
    int currentPoint;
    int targetBoardIndex = 0;
    bool isTracing = false;
    bool missed;
    bool lost;
    [HideInInspector] public bool onSpline = false;
    public override void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);
        endTargetObject = endTargetModelSelect.SelectModel(side);
        endTargetObject.GetComponent<MeshRenderer>().sharedMaterials = interactableObject.GetComponent<MeshRenderer>().sharedMaterials;
        tracingMat = interactableObject.GetComponent<MeshRenderer>().sharedMaterials[0];
        endTargetObject = endTargetModelSelect.gameObject;
        splineRenderer = threadSpline.GetComponent<Renderer>();
        //switch (_eSide)
        //{
        //    case eSide.left:
        //        splineRenderer.sharedMaterial = Resources.Load("Materials/" + "SplineLeft", typeof(Material)) as Material;
        //        break;
        //    case eSide.right:
        //        splineRenderer.sharedMaterial = Resources.Load("Materials/" + "SplineRight", typeof(Material)) as Material;
        //        break;
        //    case eSide.both:
        //        splineRenderer.sharedMaterial = Resources.Load("Materials/" + "SplineBoth", typeof(Material)) as Material;
        //        break;
        //    default:
        //        splineRenderer.sharedMaterial = Resources.Load("Materials/" + "SplineLeft", typeof(Material)) as Material;
        //        break;
        //}
        targetBoardIndex = boardIndex;
        currentPoint = 0;
    }
    private void Update()
    {

        splineCount += Time.deltaTime;

    }
    private void Start()
    {
        StartCoroutine(COTrace());
    }
    IEnumerator COTrace()
    {
        splineRenderer.sharedMaterial = tracingMat;
        yield return new WaitUntil(() => onSpline && splineCount < .1f);
        StartCoroutine(COScore());
        while (true)
        {
            yield return new WaitUntil(() => onSpline && splineCount < .1f);
            lost = false;
            while (onSpline && splineCount < .1f)
            {
                yield return null;
            }
            lost = true;
        }
        missed = true;
        splineRenderer.sharedMaterial = failedTraceMat;
        endTargetObject.SetActive(false);
        isTracing = false;
    }
    IEnumerator COScore()
    {
            float count = 0;
        while (!missed)
        {
            yield return new WaitUntil(()=> !lost);
            if (count >= .49f)
            {
                APManager.Instance.IncreaseAP(.1f, false);
                count = 0;
            }

            if (interactable.side == eSide.left)
            {
                APVFXManager.Instance.APVfxSpawnNagini(AvatarManager.Instance.avatarObjects[(int)eSide.left].transform.position, 1);
            }
            else if (side == eSide.right)
            {
                APVFXManager.Instance.APVfxSpawnYata(AvatarManager.Instance.avatarObjects[(int)eSide.right].transform.position, 1);
            }
            while (count < .5f)
            {
                HapticsManager.Instance.TriggerSimpleVibration(interactable.side, .1f, .1f);
                yield return new WaitForSeconds(.1f);
                count += .1f;
                if (lost) break;
            }
        }
    }
    public override void InteractableMissed()
    {
        if (currentPoint <= interactable.multiPoints.Length && LevelManager.Instance.GetSpawnedBoard(targetBoardIndex, LevelManager.currentStageIndex) != null)
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
            Debug.Log("threadedMissedfallback");
            base.InteractableMissed();
        }
    }

    public override void AvatarCollision(AvatarBehavior avatarBehavior)
    {
        if(!missed)
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
                interactableRenderer.gameObject.SetActive(false);
            }
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
            tmpObject.transform.SetParent(this.transform);
            tmpObject.transform.localPosition = this.transform.InverseTransformPoint(Vector3.zero);
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
        endTargetModelSelect.gameObject.SetActive(true);
        endTargetObject.gameObject.transform.SetParent(threadPositions[threadPositions.Count-1].transform);
        endTargetObject.gameObject.transform.localPosition = Vector3.zero;
        ThreadSplineUpdate();
    }
}
