using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Rendering;
public enum eBus { Master, SFX, Music}
public class AudioManager : MonoBehaviour
{
    GameManager gm;
    public static AudioManager Instance;

    public float masterVolume = .4f;
    public float musicVolume = .4f;
    public float sFXVolume = .4f;
    public FMOD.Studio.Bus Master;
    public FMOD.Studio.Bus SFX;
    public FMOD.Studio.Bus Music;

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
    public EventReference music_level_Fail;
    public EventReference music_level_Complete;

    [Space]
    [Header("SFX: Avatars")]
    public EventReference sfx_avatar_yataHit;
    public EventReference sfx_avatar_naginiHit;
    public EventReference sfx_avatar_evolveStart;
    public EventReference sfx_avatar_evolveFail;
    public EventReference sfx_avatar_evolveSuccess;

    [Space]
    [Header("SFX: Sigils")]
    public EventReference sfx_sigils_sigilTickUp;
    public EventReference sfx_sigils_sigilTickDown;
    public EventReference sfx_sigils_sigilDrainLoop;

    [Space]
    [Header("SFX: Targets")]
    public EventReference sfx_target_hit;
    public EventReference sfx_target_miss;
    public EventReference sfx_target_threadedLoop;
    public EventReference sfx_target_multiHit;

    [Space]
    [Header("SFX: Obstacles")]
    public EventReference sfx_obstacle_hit;

    [Space]
    [Header("SFX: FrontEnd")]
    public EventReference sfx_frontEnd_buttonPressed;
    public EventReference sfx_frontEnd_levelOrbPressed;
    public EventReference sfx_frontEnd_levelStart;
    public EventReference sfx_frontEnd_orbSelectionTransition;
    public EventReference sfx_frontEnd_orbHoverExploration;
    public EventReference sfx_frontEnd_orbHoverFreedom;
    public EventReference sfx_frontEnd_menuHoverLarge;
    public EventReference sfx_frontEnd_menuHoverMedium;
    public EventReference sfx_frontEnd_menuHoverSmall;
    public EventReference sfx_frontEnd_menuHoverExitLarge;
    public EventReference sfx_frontEnd_menuHoverExitMedium;
    public EventReference sfx_frontEnd_menuHoverExitSmall;
    public EventReference sfx_frontEnd_menuTransition;

    [Space]
    [Header("SFX: Pause")]
    public EventReference sfx_pause_menuOpen;
    public EventReference sfx_pause_menuClose;
    public EventReference sfx_pause_countdown;
    // This will be for when the orb reaches the player's chest and the scene transitions. This sound can be for the play button too. 

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
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX Bus");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music Bus");

        SetVolume(eBus.Master, PlayerPrefs.GetFloat("saveAll", .4f));
        SetVolume(eBus.SFX, PlayerPrefs.GetFloat("saveMusic", .4f));
        SetVolume(eBus.Music, PlayerPrefs.GetFloat("saveSFX", .4f));
    }

    public FMOD.Studio.EventInstance PlaySFX(EventReference sfxEvent)
    {
        FMOD.Studio.EventInstance sfxInstance;

        sfxInstance = FMODUnity.RuntimeManager.CreateInstance(sfxEvent);
        sfxInstance.start();
        return sfxInstance;
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
    public void SetVolume(eBus _bus, float _value)
    {
        FMOD.Studio.Bus tmpBus;
        switch (_bus)
        {
            case eBus.Master:
                tmpBus = Master;
                break;
            case eBus.SFX:
                tmpBus = SFX;
                break;
            case eBus.Music:
                tmpBus = Music;
                break;
            default:
                break;
        }
        float _newVolume = _value;

        if (_newVolume > 1.25f)
        {
            _newVolume = 1.25f;
        }

        switch (_bus)
        {
            case eBus.Master:
                masterVolume = _newVolume;
                Master.setVolume(masterVolume);
                break;
            case eBus.SFX:
                sFXVolume = _newVolume;
                SFX.setVolume(sFXVolume);
                break;
            case eBus.Music:
                musicVolume = _newVolume;
                Music.setVolume(musicVolume);
                break;
            default:
                break;
        }

        print("set "+_bus+" volume to " + _newVolume);
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
