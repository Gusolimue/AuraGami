using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using EditorAttributes;
using UnityEngine.UIElements;

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
        isRestarting = true;
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionRestartSplash();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnResumeGameButtonPressed()
    {
        PauseManager.Instance.PauseGame(false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnLevelsButtonPressed()
    {
        CanvasManager.Instance.ShowCanvasLevelSelect();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        CanvasManager.Instance.ShowCanvasSettings();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }
    [Button, SerializeField]
    public void OnMainMenuButtonPressed()
    {
        LevelSelectManager.Instance.whichLevel = 1;
        LoadManager.Instance.isTitleScreen = 1;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
    }
}
