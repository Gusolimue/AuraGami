using System.Collections.Generic;
using UnityEngine;
using EditorAttributes;
using System;

[CreateAssetMenu(fileName = "New Level", menuName = "Create Level")]
public class SoLevel : ScriptableObject
{
    [Header("Level Info")]
    public soTrack track;
    public List<Beat> beats = new List<Beat>(0);
    public void RecordBeat()
    {
        beats.Add(new Beat(BeatManager.lastMarkerString));
    }
    [Header("Stages")]
    [SerializeField]
    List<Board> stage1;
    [SerializeField]
    List<Board> stage2;
    [SerializeField]
    List<Board> stage3;


    [Header("Timing Info")]
    public int beatsToPlayer;
    public int stage1TransitionLength;
    public int stage2TransitionLength;
    public int stage3TransitionLength;
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
    [FoldoutGroup("Advanced Tools", nameof(setStageListButtonHolder), nameof(setTestLayoutButtonHolder), nameof(iWantToDeleteMyStagePermanently))]
    [SerializeField] private VoidStructure AdvancedToolsGroup;

    [SerializeField, HideInInspector] bool iWantToDeleteMyStagePermanently;

    [ButtonField(nameof(SetStageList), "!WARNING! Set Stage List !WARNING!", 30f)]
    [SerializeField, HideInInspector] VoidStructure setStageListButtonHolder;

    void SetStageList()
    {
        if (iWantToDeleteMyStagePermanently)
        {
            string lastEvent = "";
            beatsToPlayer = stage1TransitionLength = stage2TransitionLength = stage3TransitionLength = 0;
            int tmpStage2BeatsCount = 0;
            int tmpStage3BeatsCount = 0;
            int tmpStage1Count = 0;
            int tmpStage2Count = 0;
            int tmpStage3Count = 0;
            stage1.Clear();
            stage2.Clear();
            stage3.Clear();
            for (int i = 0; i < beats.Count; i++)
            {
                if(beats[i].eventString == StageManager.stage1StartString)
                {
                    beatsToPlayer++;
                    lastEvent = beats[i].eventString;
                }
                else if(beats[i].eventString == StageManager.stage1CheckString)
                {
                    stage1TransitionLength++;
                    lastEvent = beats[i].eventString;
                }
                else if(beats[i].eventString == StageManager.stage2StartString)
                {
                    tmpStage2BeatsCount++;
                    lastEvent = beats[i].eventString;
                }
                else if(beats[i].eventString == StageManager.stage2CheckString)
                {
                    stage2TransitionLength++;
                    lastEvent = beats[i].eventString;
                }
                else if(beats[i].eventString == StageManager.stage3StartString)
                {
                    tmpStage3BeatsCount++;
                    lastEvent = beats[i].eventString;
                }
                else if(beats[i].eventString == StageManager.stage3CheckString)
                {
                    stage3TransitionLength++;
                    lastEvent = beats[i].eventString;
                }
                else
                {
                    if (lastEvent == StageManager.stage1StartString)
                    {
                        stage1.Add(new Board(new Interactable[0]));
                        tmpStage1Count++;
                    }
                    else if (lastEvent == StageManager.stage2StartString)
                    {
                        stage2.Add(new Board(new Interactable[0]));
                        tmpStage2Count++;
                    }
                    else if (lastEvent == StageManager.stage3StartString)
                    {
                        stage3.Add(new Board(new Interactable[0]));
                        tmpStage3Count++;
                    }
                }


            }
            if (beatsToPlayer != tmpStage2BeatsCount || beatsToPlayer != tmpStage3BeatsCount)
            {
                Debug.LogError("Incorrect track events. Beats to player do not match for each stage. Stage1: " + beatsToPlayer + ". Stage 2: "+tmpStage2BeatsCount +". Stage 3: " +tmpStage3BeatsCount+"." );
            }
            //float tmpLength = track.trackLength;
            //tmpLength /= 1000;
            //tmpLength /= 60;
            //tmpLength *= track.bpm;
            //tmpLength -= LevelManager.beatsToPlayer * 9;
            //tmpLength = Math.Clamp(tmpLength, 0, Mathf.Infinity);
            //stage1.Clear();
            //stage2.Clear();
            //stage3.Clear();
            //for (int i = 0; i < tmpLength / 3; i++)
            //{
            //    stage1.Add(new Board(new Interactable[0]));
            //    stage2.Add(new Board(new Interactable[0]));
            //    stage3.Add(new Board(new Interactable[0]));
            //}
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

[System.Serializable]
public class Beat
{
    public string eventString;

    public Beat(string _eventString)
    {
        eventString = _eventString;
    }
}

