using UnityEngine;
using UnityEngine.UI;

public class FrontEnd : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneTransitionManager.Instance.SceneTransitionSplash();
        SceneMgr.Instance.SceneTransition();
    }

    public void OnLevelsButtonPressed()
    {
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        CanvasManager.Instance.ShowCanvasSettings();
        Destroy(this.gameObject);
    }

    public void OnQuitButtonPressed() // Will exit game (works for builds only).
    {
        Application.Quit();
    }
}
