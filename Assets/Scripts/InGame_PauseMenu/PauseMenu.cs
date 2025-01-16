using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    private void Awake()
    {
        Instance = this;    
    }

    public void OnRestartGameButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(this.gameObject);
    }

    public void OnResumeGameButtonPressed()
    {
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
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
        SceneMgr.Instance.LoadScene(eScene.frontEnd);
    }
}
