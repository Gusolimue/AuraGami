using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FrontEnd : MonoBehaviour
{
    public static FrontEnd Instance;

    [SerializeField] GameObject tutorialButton;
    [SerializeField] Slider[] connectors;
    public bool isTutorial;

    [Header("Connector Behavior")]
    public float targetValue = 1f;
    public float fillSpeed = 4f;
    private bool isFilling;

    private void Awake()
    {
        Instance = this;
        if (isTutorial == true) tutorialButton.SetActive(true);
        else tutorialButton.SetActive(false);
        foreach (Slider slider in connectors)
        {
            slider.value = 0f;
        }
        StartCoroutine(FillConnectors());
    }

    private void Update()
    {
        foreach (Slider slider in connectors)
        {
            if (slider == null) continue;

           if (isFilling) slider.value = Mathf.MoveTowards(slider.value, targetValue, fillSpeed * Time.deltaTime);
        }
    }

    public void OnPlayButtonPressed()
    {
        Debug.Log("Play Level!");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnLevelsButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnCreditsButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        CanvasManager.Instance.ShowCanvasCredits();
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        CanvasManager.Instance.ShowCanvasSettings();
        Destroy(this.gameObject);
    }

    public void OnTutorialButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        CanvasManager.Instance.ShowCanvasFEPlaytestTutorial();
        Destroy(this.gameObject);
    }

    public void OnQuitButtonPressed() // Will exit game (works for builds only).
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        Application.Quit();
    }

    private IEnumerator FillConnectors()
    {
        isFilling = false;
        yield return new WaitForSeconds(1);
        isFilling = true;
    }
}
