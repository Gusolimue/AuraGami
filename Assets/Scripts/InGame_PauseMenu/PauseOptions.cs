using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseOptions : MonoBehaviour
{
    public static PauseOptions Instance;

    [Header("Connector Behavior")]
    [SerializeField] public Slider[] connectors;
    public float targetValue = 1f;
    public float fillSpeed = 4f;
    private bool isFilling;
    public bool isLevelProgress;

    public bool isRestarting;

    private void Awake()
    {
        Instance = this;
        isRestarting = false;

        if (!isLevelProgress)
        {
            foreach (Slider slider in connectors)
            {
                slider.value = 0f;
            }
            StartCoroutine(FillConnectors());
        }
    }

    private void Update()
    {
        foreach (Slider slider in connectors)
        {
            if (slider == null) continue;

            if (isFilling) slider.value = Mathf.MoveTowards(slider.value, targetValue, fillSpeed * Time.deltaTime);
        }
    }

    public void OnRestartGameButtonPressed()
    {
        isRestarting = true;
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionRestartSplash();

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnResumeGameButtonPressed()
    {
        PauseManager.Instance.PauseGame(false);

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        PauseMenu.Instance.InstantiateSettingsMenu();
        Destroy(this.gameObject);

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }

    public void OnMainMenuButtonPressed()
    {
        LoadManager.Instance.isTitleScreen = 1;

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(2, 1);
    }

    private IEnumerator FillConnectors()
    {
        isFilling = false;
        yield return new WaitForSeconds(.5f);
        isFilling = true;
    }

    public void DestroyMenu()
    {
        Destroy(this.gameObject);
    }
}
