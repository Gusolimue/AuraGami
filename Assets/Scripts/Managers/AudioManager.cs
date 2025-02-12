using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    GameManager gm;
    public static AudioManager Instance;

    public float masterVolume = .4f;
    FMOD.Studio.Bus Master;

    public string currentMusicName = "null";
    public FMOD.Studio.EventInstance currentMusicInstance;
    public int currentMusicLoopPoint;

    public string lastMusicPlaying;
    public int lastMusicLoopPoint;

    //public FMOD.Studio.EventInstance[] currentSfxInstances;

    [Space]
    [Header("Music: Menu")]
    public EventReference music_menu_titlescreen;

    [Space]
    [Header("Music: Level")]
    public EventReference music_level_Freedom;

    [Space]
    [Header("SFX: Avatars")]
    public EventReference sfx_avatar_sigilFill;

    [Space]
    [Header("SFX: Targets")]
    public EventReference sfx_target_hit;
    public EventReference sfx_target_miss;
    public EventReference sfx_target_follow;
    public EventReference sfx_target_chain;

    [Space]
    [Header("SFX: Obstacles")]
    public EventReference sfx_obstacle_hit;

    [Space]
    [Header("SFX: FrontEnd")]
    public EventReference sfx_frontEnd_buttonPressed;

    void Awake()
    {
        // Music keeps playing between scenes due to this object not being destroyed.
        Instance = this;

       /* if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }*/

    }

    void Start()
    {
        //gm = GameManager.gm;

        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        SetVolumeMaster(.4f);
    }

    public void PlaySFX(EventReference sfxEvent)
    {
        FMOD.Studio.EventInstance sfxInstance;

        sfxInstance = FMODUnity.RuntimeManager.CreateInstance(sfxEvent);
        sfxInstance.start();
    }

    public void PlayMusic(EventReference musicEvent, int loopPoint = 0)
    {
        lastMusicPlaying = musicEvent.ToString();
        lastMusicLoopPoint = loopPoint;

        //if(gm.saveManager.musicEnabled == false)
        //{
        //    return;
        //}

        // don't replay the current music
        if (currentMusicName == musicEvent.ToString() && currentMusicLoopPoint == loopPoint)
        {
            Debug.Log("Current song (<color=yellow>" + currentMusicName + "</color>) and current loop point (<color=yellow>" + currentMusicLoopPoint + "</color>) are the same as the trigger; do not play.");
            return;
        }

        // if the current music is the same song, but is currently at a different loop point
        if (currentMusicName == musicEvent.ToString() && currentMusicLoopPoint != loopPoint)
        {
            if(CheckPlaybackState(currentMusicInstance) == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                currentMusicInstance.start();
            }

            currentMusicInstance.setParameterByName("musicDestinationEvent", loopPoint);
            currentMusicLoopPoint = loopPoint;
            Debug.Log("Current song (<color=yellow>" + currentMusicName + "</color>) is the same as trigger, but loop point (<color=yellow>" + currentMusicLoopPoint + "</color>) is different from trigger; calling new loop point.");
            return;
        }

        // otherwise, play a new song and set it to the current song

        currentMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        FMOD.Studio.EventInstance musicInstance;
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
        musicInstance.setParameterByName("musicDestinationEvent", loopPoint);

        currentMusicName = musicEvent.ToString();
        currentMusicLoopPoint = loopPoint;
        currentMusicInstance = musicInstance;

        Debug.Log("Playing new song (<color=yellow>" + currentMusicName + "</color>) at loop point (<color=yellow>" + currentMusicLoopPoint + "</color>)");
    }

    public void StopMusic()
    {
        if (CheckPlaybackState(currentMusicInstance) == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            print("stopping current music");
            currentMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentMusicName = "null";
        }
    }

    public void SetVolumeMaster(float _value)
    {
        float _newMasterVolume = _value;

        if (_newMasterVolume > 1.25f)
        {
            _newMasterVolume = 1.25f;
        }

        masterVolume = _newMasterVolume;
        Master.setVolume(masterVolume);

        print("set master volume to " + masterVolume);
    }

    public FMOD.Studio.PLAYBACK_STATE CheckPlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE ps;
        instance.getPlaybackState(out ps);
        return ps;
    }

}
