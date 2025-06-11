using UnityEngine;
using UnityEngine.Events;
using System.Runtime.InteropServices;
using FMODUnity;
using System;
using EditorAttributes;
using System.Collections.Generic;

public class BeatManager : MonoBehaviour
{
    public static BeatManager Instance;

    [SerializeField]
    EventReference music;
    [SerializeField]
    SoLevel levelToRecordTo;
    public TimelineInfo timelineInfo = null;
    GCHandle timelineHandle;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate beatUpdated;

    public delegate void MarkerEventDelegate();
    public static event MarkerEventDelegate markerUpdated;

    private FMOD.Studio.EventInstance musicInstance;
    private FMOD.Studio.EVENT_CALLBACK beatCallback;

    public static int lastBeat = 0;
    public static string lastMarkerString = null;

    private void Awake()
    {
        Instance = this;
        //musicInstance = RuntimeManager.CreateInstance(music);
        //musicInstance.start();
    }
    [/*Button,*/ SerializeField]
    public void PauseMusicTMP(bool _paused)
    {
        musicInstance.setPaused(_paused);
    }
    [Button, SerializeField]
    public void StartSong()
    {
        if(LevelManager.Instance != null)
        {
            music = LevelManager.Instance.level.track.trackReference;
        }
        musicInstance = RuntimeManager.CreateInstance(music);
        musicInstance.start();
        timelineInfo = new TimelineInfo();
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT
            | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        start = true;
    }
    [Button, SerializeField]
    public void RecordSongData()
    {
        levelToRecordTo.beats = new List<Beat>(0);
        beatUpdated += levelToRecordTo.RecordBeat;
        music = levelToRecordTo.track.trackReference;
        StartSong();
        FMOD.Studio.EventDescription eventDescription;
        musicInstance.getDescription(out eventDescription);
        eventDescription.getLength(out levelToRecordTo.track.trackLength);
    }
    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }
    private void Start()
    {
        //timelineInfo = new TimelineInfo();
        //beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        //timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        //musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        //musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT
        //    | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }
    public void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        timelineHandle.Free();
    }
    bool start = false;
    private void Update()
    {
        if(start)
        {
            if (lastMarkerString != timelineInfo.lastMarker)
            {
                lastMarkerString = timelineInfo.lastMarker;

                if (markerUpdated != null)
                {
                    markerUpdated();
                }
            }

            if (lastBeat != timelineInfo.currentBeat)
            {
                lastBeat = timelineInfo.currentBeat;

                if (beatUpdated != null)
                {
                    beatUpdated();
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if(start)
        {
            GUILayout.Box($"Current beat = {timelineInfo.currentBeat}, Last Marker = {(string)timelineInfo.lastMarker}");
        }
    }
#endif
    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE _type, IntPtr _instancePtr, IntPtr _parameterPtr )
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(_instancePtr);

        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback Error: "+ result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (_type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure
                            (_parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure
                            (_parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
    //[SerializeField] private float bpm;
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private Intervals[] intervals;
    //private void Update()
    //{
    //    foreach (Intervals interval in intervals)
    //    {
    //        float sampledTime = audioSource.timeSamples / (audioSource.clip.frequency * interval.GetIntervalLength(bpm));
    //        interval.CheckForNewInterval(sampledTime);
    //    }
    //}

}

//[System.Serializable]
//public class Intervals
//{
//    [SerializeField] private float steps;
//    [SerializeField] UnityEvent trigger;
//    int lastInterval;
//    public float GetIntervalLength(float _bpm)
//    {
//        return 60f / (_bpm * steps);
//    }
//    public void CheckForNewInterval (float interval)
//    {
//        if (Mathf.FloorToInt(interval) != lastInterval)         
//        {
//            lastInterval = Mathf.FloorToInt(interval);
//            trigger.Invoke();
//        }
//    }
//}
