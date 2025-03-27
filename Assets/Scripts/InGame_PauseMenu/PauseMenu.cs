using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using EditorAttributes;

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
        //PauseManager.Instance.PauseGame(false); //Tmp
        isRestarting = true;
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionRestartSplash();
        Destroy(this.gameObject);
    }

    public void OnResumeGameButtonPressed()
    {
        //PauseManager.Instance.PauseGame(false); //Tmp
        PauseManager.Instance.yataAvatar.SetActive(true);
        PauseManager.Instance.naginiAvatar.SetActive(true);
        PauseManager.Instance.StartCountdown();
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
    [Button, SerializeField]
    public void OnMainMenuButtonPressed()
    {
        LevelSelectManager.Instance.whichLevel = 0;
        PauseManager.Instance.PauseGame(false);
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
    }
}
