using UnityEngine;
using FMODUnity;
using System.Collections.Generic;
using EditorAttributes;
//This is not currently used, but will be to hold data for each playable song
[CreateAssetMenu(fileName = "New Track", menuName = "Create Track")]
[System.Serializable]
public class soTrack : ScriptableObject
{
    public string trackName;
    public EventReference trackReference;
    public int trackLength;
    public float beatLength;
    public int bpm;

    //[ButtonField(nameof(SetTrackLength), "!WARNING! Set Stage List !WARNING!", 30f)]
    //[SerializeField, HideInInspector] VoidStructure setStageListButtonHolder;

    //void SetTrackLength()
    //{
    //    FMOD.Studio.EventDescription
    //}
}
