using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FrontEnd : MonoBehaviour // This script handles all main menu button functionality. 
{
    public static FrontEnd Instance;
    public FMOD.Studio.EventInstance idleSoundInstance;
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
        LoadManager.Instance.isTutorial = false;
        if (isTutorial == true) tutorialButton.SetActive(true);
        else tutorialButton.SetActive(false);
        foreach (Slider slider in connectors)
        {
            slider.value = 0f;
        }
        StartCoroutine(FillConnectors());

        if (LoadManager.Instance.isTitleScreen == 1) AudioManager.Instance.PlayMusic(AudioManager.Instance.music_menu_titlescreen);
    }

    private void Update() // To add more flare to opening the main menu, I have the connectors between each one fill in. 
    {
        foreach (Slider slider in connectors)
        {
            if (slider == null) continue;

           if (isFilling) slider.value = Mathf.MoveTowards(slider.value, targetValue, fillSpeed * Time.deltaTime);
        }
    }

    public void OnPlayButtonPressed() // Opens the level selection menu.
    {
        Debug.Log("Play Level!");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnCreditsButtonPressed() // Opens the credits menu to view all the team members.
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        CanvasManager.Instance.ShowCanvasCredits();
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed() // Opens the settings menu.
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        CanvasManager.Instance.ShowCanvasSettings();
        Destroy(this.gameObject);
    }

    public void OnTutorialButtonPressed() // More of a temp button to fill out the space. 
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(1, 1);
        Destroy(this.gameObject);
    }

    public void OnQuitButtonPressed() // Will exit game (works for builds only).
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        Application.Quit();
    }

    private IEnumerator FillConnectors() // Gives a slight delay to filling the connectors. This is to time it with the buttons settling.
    {
        isFilling = false;
        yield return new WaitForSeconds(1);
        isFilling = true;
    }
}
