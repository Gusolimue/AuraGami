using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EditorAttributes;
using TMPro;

public class FrontEnd : MonoBehaviour
{
    public static FrontEnd Instance;

    [SerializeField] GameObject tutorialButton;
    public bool isTutorial;

    private void Awake()
    {
        Instance = this;
        if (isTutorial == true) tutorialButton.SetActive(true);
        else tutorialButton.SetActive(false);
    }

    public void OnPlayButtonPressed()
    {
        LevelSelectManager.Instance.whichLevel = 2;
        Debug.Log("Play Level!");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
    }

    public void OnLevelsButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasSettings();
        Destroy(this.gameObject);
    }

    public void OnTutorialButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasFEPlaytestTutorial();
        Destroy(this.gameObject);
    }

    public void OnQuitButtonPressed() // Will exit game (works for builds only).
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Application.Quit();
    }
}
