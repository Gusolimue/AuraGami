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
        //this.gameObject.transform.localPosition = new Vector3(-.13f, 4.15f, 5.77f);
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
        //Destroy(this.gameObject);
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

    public void DestroyMenu()
    {
        Destroy(this.gameObject);
    }
}
