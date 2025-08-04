using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameplaySettingsManager : MonoBehaviour
{
    public static GameplaySettingsManager Instance;

    [Space]
    [Header("Player Circle/Slider")]
    [SerializeField] public Slider[] playCircleSlider;
    [SerializeField] Image playCircleDemo;
    [SerializeField] GameObject playerCircle;
    [SerializeField] Color[] colorChanges;
    public float playCircleSizeSliderValue;
    public float playCircleHeightSliderValue;
    private bool demoOn;
    private bool inGameDemoOn;

    [Space]
    [Header("Toggle")]
    [SerializeField] Image toggleFill;
    [SerializeField] Image hoverFill;
    [SerializeField] Color[] toggleColors;
    public bool isToggled;
    public int toggleNum;
    private bool isHover;

    private void Awake()
    {
        Instance = this;
        CanvasManager.Instance.playerCircle.gameObject.SetActive(true);

        playCircleSlider[0].value = PlayerPrefs.GetFloat("playCircleScale", .5f);
        playCircleSlider[0].onValueChanged.AddListener(ChangeSlider);
        playCircleDemo.color = colorChanges[1];
        CanvasManager.Instance.playerCircle.color = colorChanges[1];

        playCircleSlider[1].value = PlayerPrefs.GetFloat("playCircleHeight", .5f);
        playCircleSlider[1].onValueChanged.AddListener(ChangePlayerCircleHeightSlider);

        toggleNum = PlayerPrefs.GetInt("toggleCircle", 2);
        if (toggleNum == 1) toggleFill.color = colorChanges[0];
        else if (toggleNum == 2) toggleFill.color = colorChanges[2];

        Debug.Log(playCircleSlider[0].value);
    }

    private void Update()
    {
        if (demoOn) playCircleDemo.color = Color.Lerp(playCircleDemo.color, colorChanges[0], Time.deltaTime * 5);
        else playCircleDemo.color = Color.Lerp(playCircleDemo.color, colorChanges[1], Time.deltaTime * 5);

        if (inGameDemoOn) CanvasManager.Instance.playerCircle.color = Color.Lerp(CanvasManager.Instance.playerCircle.color, colorChanges[0], Time.deltaTime * 5);
        else CanvasManager.Instance.playerCircle.color = Color.Lerp(CanvasManager.Instance.playerCircle.color, colorChanges[1], Time.deltaTime * 5);

        if (toggleNum == 1) toggleFill.color = Color.Lerp(toggleFill.color, toggleColors[0], Time.deltaTime * 5);
        else if (toggleNum == 2) toggleFill.color = Color.Lerp(toggleFill.color, toggleColors[1], Time.deltaTime * 5);

        hoverFill.color = Color.Lerp(hoverFill.color, isHover ? toggleColors[2] : toggleColors[1], Time.deltaTime * 5);
    }

    public void ChangeSlider(float value)
    {
        //Debug.Log("Saving slider value: " + value);

        playCircleSizeSliderValue = value;
        PlayerPrefs.SetFloat("playCircleScale", value);
        PlayerPrefs.Save();

        if (playCircleDemo != null || CanvasManager.Instance.playerCircle != null) 
        {
            if (CanvasManager.Instance.isInGameDemo) CanvasManager.Instance.playerCircle.transform.localScale = Vector3.one * value;
            else playCircleDemo.transform.localScale = Vector3.one * value;
        }
        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 1));
        }
    }

    public void ChangePlayerCircleHeightSlider(float value)
    {
        playCircleHeightSliderValue = value;
        PlayerPrefs.SetFloat("playCircleHeight", value);
        PlayerPrefs.Save();

        if (playCircleDemo != null || CanvasManager.Instance.playerCircle != null)
        {
            if (CanvasManager.Instance.isInGameDemo)
            {
                Vector3 inGameNewYPos = CanvasManager.Instance.playerCircle.transform.position;
                inGameNewYPos.y = value * 1f;
                CanvasManager.Instance.playerCircle.transform.position = inGameNewYPos;
            }
            else
            {
                Vector3 newYPosition = playCircleDemo.transform.position;
                newYPosition.y = value * 1f;
                playCircleDemo.transform.position = newYPosition;
            }
        }

        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 1));
        }
    }

    public void TogglePlayerCircle()
    {

        if (!isToggled)
        {
            isToggled = true;
            toggleNum = 1;
            PlayerPrefs.SetInt("toggleCircle", toggleNum);
            PlayerPrefs.Save();
        }
        else
        {
            isToggled = false;
            toggleNum = 2;
            PlayerPrefs.SetInt("toggleCircle", toggleNum);
            PlayerPrefs.Save();
        }

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_levelOrbPressed);
        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 1));
        }
    }

    public void OnHover()
    {
        isHover = true;

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void ExitHover()
    {
        isHover = false;
    }

    public void IncreasePlayerCircleSize()
    {
        playCircleSlider[0].value += .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        if (CanvasManager.Instance.isInGameDemo) StartCoroutine(ActivateInGameCircleDemo());
        else StartCoroutine(ActivateCircleDemo());
    }

    public void DecreasePlayerCircleSize()
    {
        playCircleSlider[0].value -= .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        if (CanvasManager.Instance.isInGameDemo) StartCoroutine(ActivateInGameCircleDemo());
        else StartCoroutine(ActivateCircleDemo());
    }

    public void IncreasePlayerCircleHeight()
    {
        playCircleSlider[1].value += .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        if (CanvasManager.Instance.isInGameDemo) StartCoroutine(ActivateInGameCircleDemo());
        else StartCoroutine(ActivateCircleDemo());
    }

    public void DecreasePlayerCircleHeight()
    {
        playCircleSlider[1].value -= .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        if (CanvasManager.Instance.isInGameDemo) StartCoroutine(ActivateInGameCircleDemo());
        else StartCoroutine(ActivateCircleDemo());
    }

    IEnumerator ActivateCircleDemo()
    {
        demoOn = true;
        yield return new WaitForSeconds(5);
        demoOn = false;
    }

    IEnumerator ActivateInGameCircleDemo()
    {
        inGameDemoOn = true;
        yield return new WaitForSeconds(5);
        inGameDemoOn = false;
    }

    public void DefaultSettings()
    {
        playCircleSlider[0].value = 1.25f;
        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 1));
        }

        playCircleSlider[1].value = 1.25f;
        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 1));
        }

        demoOn = false;
        toggleNum = 2;
        PlayerPrefs.SetInt("toggleCircle", toggleNum);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_levelOrbPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
    }
}
