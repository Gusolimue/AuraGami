using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public bool isRestarting;

    private void Awake()
    {
        Instance = this;
        isRestarting = false;
    }

    public void OnRestartGameButtonPressed()
    {
        PauseManager.Instance.PauseGame(false); //Tmp
        isRestarting = true;
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
        Destroy(this.gameObject);
    }

    public void OnResumeGameButtonPressed()
    {
        PauseManager.Instance.PauseGame(false); //Tmp
        StartCoroutine(PauseManager.Instance.Countdown(3));
        PauseManager.Instance.isPaused = false;
        Destroy(this.gameObject);
    }

    public void OnLevelsButtonPressed()
    {
        //NewAudioManager.Instance.frontEndButtonSFX.Play();
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        //NewAudioManager.Instance.frontEndButtonSFX.Play();
        CanvasManager.Instance.ShowCanvasSettings();
        Destroy(this.gameObject);
    }
    
    public void OnMainMenuButtonPressed()
    {
        //SceneMgr.Instance.IntoFrontEndSceneTransition();
        PauseManager.Instance.PauseGame(false);
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
    }
}
