using UnityEngine;
using System;
using System.Collections.Generic;
using EditorAttributes;

public class Level : MonoBehaviour
{
    [Header("Level Info")]
    //call error if two targets are placed on the same spot
    public soTrack soTrack;
    private FMOD.Studio.System musicInstance;
    [Button, SerializeField]
    void SetStageList()
    {
        float tmpLength = soTrack.trackLength;
        tmpLength /= 1000;
        tmpLength /= 60;
        tmpLength *= soTrack.bpm;
        stage1.Clear();
        stage2.Clear();
        stage3.Clear();
        for (int i = 0; i < tmpLength/3; i++)
        {
            stage1.Add(new Board());
            stage2.Add(new Board());
            stage3.Add(new Board());
        }
        // button to set board amount based on bpm/time sig for song entered
        //stages should be separate lists
        //if a list is already created, it should add or remove elements for the list to add up
        //otherwise, it will make a new blank list of boards with targets
    }
    [Header("Stages")]
    [SerializeField]
    List<Board> stage1;
    [SerializeField]
    List<Board> stage2;
    [SerializeField]
    List<Board> stage3;
    [Button, SerializeField]
    void ShowLevel()
    {
        GameObject tmpLastBoard;
        if (levelPreview != null)
        {
            DestroyImmediate(levelPreview);
        }
        levelPreview = new GameObject("Level Preview");
        levelPreview.transform.parent = this.transform;
        stage1Preview = new GameObject("Stage 1 Preview");
        stage1Preview.transform.parent = levelPreview.transform;
        stage2Preview = new GameObject("Stage 2 Preview");
        stage2Preview.transform.parent = levelPreview.transform;
        stage3Preview = new GameObject("Stage 3 Preview");
        stage3Preview.transform.parent = levelPreview.transform;
        stage3Preview.transform.position = levelPreview.transform.position = stage1Preview.transform.position = stage2Preview.transform.position = Vector3.zero;
        for (int i = 0; i < stage1.Count; i++)
        {
            SpawnBoard(i, stage1, stage1Preview.transform);
            currentBoard.transform.position += new Vector3(0, 0, .2f * i);
        }
        tmpLastBoard = currentBoard;
        for (int i = 0; i < stage1.Count; i++)
        {
            SpawnBoard(i, stage2, stage2Preview.transform);
            currentBoard.transform.position += tmpLastBoard.transform.position + new Vector3(0, 0, .2f * i);
        }
        tmpLastBoard = currentBoard;
        for (int i = 0; i < stage1.Count; i++)
        {
            SpawnBoard(i, stage3, stage3Preview.transform);
            currentBoard.transform.position += tmpLastBoard.transform.position + new Vector3(0, 0, .2f * i);
        }
    }
    private void Awake()
    {
        if(levelPreview != null)
        {
            levelPreview.SetActive(false);
        }
    }
    //spawns a board with its targets when called
    [HideInInspector] public GameObject currentBoard;
    public GameObject levelPreview;
    GameObject stage1Preview;
    GameObject stage2Preview;
    GameObject stage3Preview;
    public void SpawnBoard(int num, List<Board> _stage, Transform transform)
    {
        currentBoard = Instantiate(Resources.Load("InGame/" + "Interactables/" + "BoardPrefab")
            as GameObject, transform);
        foreach (var Target in _stage[num].interactables)
        {
            SpawnTarget(Target);
        }
        if(_stage[num].interactables.Length == 0)
        {
            currentBoard.name = "Empty Board";
        }
    }

    //called to spawn every individual target when a board is spawned
    void SpawnTarget(Interactable _target)
    {
        GameObject tmpObject;
        switch (_target.interactableType)
        {
            case eTargetType.regularTarget:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "regularTargetPrefab")
            as GameObject, currentBoard.transform);
                break;
            case eTargetType.multihitTarget:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "multiHitTargetPrefab")
            as GameObject, currentBoard.transform);
                break;
            case eTargetType.threadedTarget:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "regularTargetPrefab")
            as GameObject, currentBoard.transform);
                break;
            case eTargetType.precisionTarget:
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "regularTargetPrefab")
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
        tmpObject.GetComponent<BaseInteractableBehavior>().InitInteractable(_target.side, _target);
        Quaternion tmpRot = new Quaternion();
        tmpRot.eulerAngles = new Vector3(0, 0, _target.interactableAngle);
        tmpObject.transform.localRotation *= tmpRot;
        tmpObject.transform.Translate(Vector3.up * _target.interactableDistance) ;
        tmpObject.transform.localRotation = Quaternion.identity;
    }
    [Header("Level Editing")]
    //fix the organization, also buttons are not showing where they should
    [SerializeField, Dropdown(nameof(stageDropdown))] public int stageSelection;
    private int[] stageDropdown = new int[] { 1, 2, 3 };
    [SerializeField, HorizontalGroup(true, nameof(startIndex), nameof(endIndex))] private VoidStructure groupHolder;
    [SerializeField, HideProperty] int startIndex;
    [SerializeField, HideProperty] int endIndex;

    [SerializeField, HorizontalGroup(true, nameof(copyButtonHolder), nameof(pasteButtonHolder))] private VoidStructure buttonGroupHolder;
    [ButtonField(nameof(CopyPressed), "Copy", 30f)]
    [SerializeField, HideInInspector] VoidStructure copyButtonHolder;
    void CopyPressed()
    {
        copiedSelection.Clear();
        int tmpIndex = endIndex;
        if (GetStage(stageSelection).Count < endIndex) tmpIndex = stage1.Count;
        copiedSelection.AddRange(stage1.GetRange(startIndex, tmpIndex));
    }
    public List<Board> GetStage(int _index)
    {
        switch (_index)
        {
            case 1:
                return stage1;
            case 2:
                return stage2;
            case 3:
                return stage3;
            default:
                return stage1;
        }
    }
    [SerializeField] List<Board> copiedSelection;
    [SerializeField] int selectionToPaste;
    [ButtonField(nameof(PastePressed), "Paste", 30f)]
    [SerializeField, HideInInspector] VoidStructure pasteButtonHolder;
    void PastePressed()
    {
        for (int i = 0; i < copiedSelection.Count; i++)
        {
            if (GetStage(stageSelection).Count > startIndex + i)
            {
                GetStage(stageSelection)[startIndex + i] = copiedSelection[i];
            }
        }
    }
    //[ButtonField(nameof(Undo))]
    //[SerializeField, HideInInspector] VoidStructure undoButtonHolder;
    //void Undo(){}
    //[ButtonField(nameof(Redo))]
    //[SerializeField, HideInInspector] VoidStructure redoButtonHolder;
    //void Redo(){}
}


// a class to hold one "plane" of interactables
[System.Serializable]
public class Board
{
    public Interactable[] interactables;
}
//an interactable to be set for a board
public enum eTargetType { regularTarget, multihitTarget, threadedTarget, precisionTarget,  regularObstacle }
[System.Serializable]
public class Interactable
{
    public eTargetType interactableType;
    public eSide side;
    [Range(0, 359)]
    public int interactableAngle;
    [Range(0f, 1f)]
    public float interactableDistance;

    [ShowField(nameof(interactableType), eTargetType.multihitTarget), HideProperty] public TargetPoints[] multiPoints;

    [ShowField(nameof(interactableType), eTargetType.threadedTarget)] public TargetPoints[] threadedPoints;
}
[System.Serializable]
public class TargetPoints
{
    public int boardsMoved;
    [Range(0, 359)]
    public int interactableAngle;
    [Range(0f, 1f)]
    public float interactableDistance;
}
