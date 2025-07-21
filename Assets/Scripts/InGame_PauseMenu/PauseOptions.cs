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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnResumeGameButtonPressed()
    {
        PauseManager.Instance.PauseGame(false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        PauseMenu.Instance.InstantiateSettingsMenu();
        Destroy(this.gameObject);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }

    // [Button, SerializeField]
    public void OnMainMenuButtonPressed()
    {
        LoadManager.Instance.isTitleScreen = 1;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(2, 0);
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
