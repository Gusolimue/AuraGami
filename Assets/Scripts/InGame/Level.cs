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
    [Header("Stages")]
    [SerializeField]
    List<Board> stage1;
    [SerializeField]
    List<Board> stage2;
    [SerializeField]
    List<Board> stage3;
    [ButtonField(nameof(ShowLevel), "Show Level Preview", 30f)]
    [SerializeField] VoidStructure showLevelButtonHolder;

    int boardCount;
    void ShowLevel()
    {
        boardCount = 0;
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
            SpawnBoard(i, stage1, stage1Preview.transform, 0);
            currentBoard.transform.position += new Vector3(0, 0, .2f * i);
        }
        tmpLastBoard = currentBoard;
        for (int i = 0; i < stage2.Count; i++)
        {
            SpawnBoard(i, stage2, stage2Preview.transform, 1);
            currentBoard.transform.position += tmpLastBoard.transform.position + new Vector3(0, 0, .2f * i);
        }
        tmpLastBoard = currentBoard;
        for (int i = 0; i < stage3.Count; i++)
        {
            SpawnBoard(i, stage3, stage3Preview.transform, 2);
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
    public void SpawnBoard(int num, List<Board> _stage, Transform transform, int _stageIndex = 0)
    {
        currentBoard = Instantiate(Resources.Load("InGame/" + "Interactables/" + "BoardPrefab")
            as GameObject, transform);
        foreach (var Target in _stage[num].interactables)
        {
            SpawnTarget(Target, _stageIndex);
        }
        if(_stage[num].interactables.Length == 0)
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
                tmpObject = Instantiate(Resources.Load("InGame/" + "Interactables/" + "threadedTargetPrefab")
            as GameObject, currentBoard.transform);
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

        //int stageCount = 1; // Used to give the target a reference for the stage it's in

        //if (boardCount < GetStage(2).Count * 2)
        //{
        //    stageCount = 2;
        //}
        //else if (boardCount < GetStage(3).Count * 3)
        //{
        //    stageCount = 3;
        //}

        tmpObject.GetComponent<BaseInteractableBehavior>().InitInteractable(_target.side, _index,  boardCount, _target);
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
        tmpSelection = new List<Board>(0);
        tmpSelection.AddRange(GetStage(stageSelection).GetRange(startIndex, Math.Clamp(endIndex + 1, 0 , GetStage(stageSelection).Count)));
        for (int i = 0; i < tmpSelection.Count; i++)
        {
            if(tmpSelection[i].interactables != null)
            {
                copiedSelection.Add(new Board(tmpSelection[i].interactables));
            }
            else
            {
                copiedSelection.Add(new Board(new Interactable[0]));
            }
        }
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
    List<Board> tmpSelection;
    [SerializeField] int selectionToPaste;
    [ButtonField(nameof(PastePressed), "Paste", 30f)]
    [SerializeField, HideInInspector] VoidStructure pasteButtonHolder;
    void PastePressed()
    {
        for (int i = 0; i < copiedSelection.Count; i++)
        {

            if (GetStage(stageSelection).Count > startIndex + i)
            {
                GetStage(stageSelection)[startIndex + i] = new Board(copiedSelection[i].interactables);
            }
        }
    }
    //[ButtonField(nameof(Undo))]
    //[SerializeField, HideInInspector] VoidStructure undoButtonHolder;
    //void Undo(){}
    //[ButtonField(nameof(Redo))]
    //[SerializeField, HideInInspector] VoidStructure redoButtonHolder;
    //void Redo(){}
    [FoldoutGroup("Advanced Tools", nameof(setStageListButtonHolder), nameof(setTestLayoutButtonHolder), nameof(iWantToDeleteMyStagePermanently))]
    [SerializeField] private VoidStructure AdvancedToolsGroup;

    [SerializeField, HideInInspector] bool iWantToDeleteMyStagePermanently;

    [ButtonField(nameof(SetStageList), "!WARNING! Set Stage List !WARNING!", 30f)]
    [SerializeField, HideInInspector] VoidStructure setStageListButtonHolder;

    void SetStageList()
    {
        if(iWantToDeleteMyStagePermanently)
        {
            float tmpLength = soTrack.trackLength;
            tmpLength /= 1000;
            tmpLength /= 60;
            tmpLength *= soTrack.bpm;
            stage1.Clear();
            stage2.Clear();
            stage3.Clear();
            for (int i = 0; i < tmpLength / 3; i++)
            {
                stage1.Add(new Board(new Interactable[0]));
                stage2.Add(new Board(new Interactable[0]));
                stage3.Add(new Board(new Interactable[0]));
            }
            iWantToDeleteMyStagePermanently = false;
        }
        else
        {
            Debug.Log("WARNING! SET STAGE LIST DELETES YOUR CURRENT STAGES. THIS CAN NOT BE UNDONE. IF YOU WOULD LIKE TO PROCEED" +
                ", CHECK THE 'I Would Like To Delete My Stage Permanently' BOX.");
        }
        // button to set board amount based on bpm/time sig for song entered
        //stages should be separate lists
        //if a list is already created, it should add or remove elements for the list to add up
        //otherwise, it will make a new blank list of boards with targets
    }
    [ButtonField(nameof(SetTestLayout), "!WARNING! Set Test Layout !WARNING!", 30f)]
    [SerializeField, HideInInspector] VoidStructure setTestLayoutButtonHolder;

    void SetTestLayout()
    {
        if (iWantToDeleteMyStagePermanently)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int c = 0; c < GetStage(i + 1).Count; c++)
                {
                    eTargetType tmpType = eTargetType.regularTarget;
                    eSide tmpSide = eSide.left;
                    int tmpAngle = c;
                    float tmpDistance = (i + 1f) / 3f;
                    Debug.Log(tmpDistance);
                    if (c % 2 != 0)
                    {
                        tmpSide = eSide.right;
                    }
                    Interactable[] tmpInteractableArray = new Interactable[1];
                    tmpInteractableArray[0] = new Interactable(tmpType, tmpSide, tmpAngle, tmpDistance);
                    GetStage(i + 1)[c] = new Board(tmpInteractableArray);

                }
            }
            iWantToDeleteMyStagePermanently = false;
        }
        else
        {
            Debug.Log("WARNING! THIS DELETES YOUR CURRENT STAGES. THIS CAN NOT BE UNDONE. IF YOU WOULD LIKE TO PROCEED" +
                ", CHECK THE 'I Would Like To Delete My Stage Permanently' BOX.");
        }
    }


}


// a class to hold one "plane" of interactables
[System.Serializable]
public class Board
{
    public Interactable[] interactables;
    public Board(Interactable[] _interactables)
    {
        interactables = new Interactable[_interactables.Length];
        for (int i = 0; i < _interactables.Length; i++)
        {
            TargetPoints[] tmpMultipoints = null;
            if (_interactables[i].multiPoints != null)
            {
                tmpMultipoints = new TargetPoints[_interactables[i].multiPoints.Length];
                for (int c = 0; c < _interactables[i].multiPoints.Length; c++)
                {
                    tmpMultipoints[c] = new TargetPoints(_interactables[i].multiPoints[c].boardsMoved,
                        _interactables[i].multiPoints[c].interactableAngle, _interactables[i].multiPoints[c].interactableDistance);
                }
            }
            //Debug.Log(_interactables[i].interactableType);
            //Debug.Log(_interactables[i].side);
            //Debug.Log(_interactables[i].interactableAngle);
            //Debug.Log(_interactables[i].interactableDistance);
            //Debug.Log(tmpMultipoints);
            interactables[i] = new Interactable(_interactables[i].interactableType, _interactables[i].side,
                _interactables[i].interactableAngle, _interactables[i].interactableDistance, tmpMultipoints);
        }
    }
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

    public TargetPoints[] multiPoints;

    public Interactable(eTargetType _interactableType, eSide _side, int _interactableAngle, float _interactableDistance, TargetPoints[] _multiPoints = null)
    {
        interactableType = _interactableType;
        side = _side;
        interactableAngle = _interactableAngle;
        interactableDistance = _interactableDistance;
        multiPoints = _multiPoints;
    }
}
[System.Serializable]
public class TargetPoints
{
    public int boardsMoved;
    [Range(0, 359)]
    public int interactableAngle;
    [Range(0f, 1f)]
    public float interactableDistance;
    public TargetPoints(int _boardsMoved, int _interactableAngle, float _interactableDistance)
    {
        boardsMoved = _boardsMoved;
        interactableAngle = _interactableAngle;
        interactableDistance = _interactableDistance;
    }
}
