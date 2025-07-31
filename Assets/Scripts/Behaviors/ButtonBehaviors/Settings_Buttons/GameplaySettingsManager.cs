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
    private bool demoOn = false;

    [Space]
    [Header("Toggle")]
    [SerializeField] Image toggleFill;
    public bool isToggled;
    public int toggleNum;

    [Header("Reset")]
    [SerializeField] Image resetFill;
    [SerializeField] Color[] fillColor;
    private float count;
    private bool isReset;

    private void Awake()
    {
        Instance = this;

        playCircleSlider[0].value = PlayerPrefs.GetFloat("playCircleScale", .5f);
        playCircleSlider[0].onValueChanged.AddListener(ChangeSlider);
        playCircleDemo.color = colorChanges[1];

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

        if (toggleNum == 1) toggleFill.color = Color.Lerp(toggleFill.color, colorChanges[0], Time.deltaTime * 5);
        else if (toggleNum == 2) toggleFill.color = Color.Lerp(toggleFill.color, colorChanges[2], Time.deltaTime * 5);

        count += Time.deltaTime;
        if (isReset) resetFill.color = Color.Lerp(resetFill.color, fillColor[0], count / 5);
        else resetFill.color = Color.Lerp(resetFill.color, fillColor[1], count / 5);
    }

    public void ChangeSlider(float value)
    {
        //Debug.Log("Saving slider value: " + value);

        //playCircleSizeSliderValue = value;
        PlayerPrefs.SetFloat("playCircleScale", value);
        PlayerPrefs.Save();

        if (playCircleDemo != null) playCircleDemo.transform.localScale = Vector3.one * value;
        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 1));
        }
    }

    public void ChangePlayerCircleHeightSlider(float value)
    {
        //playCircleHeightSliderValue = value;
        PlayerPrefs.SetFloat("playCircleHeight", value);
        PlayerPrefs.Save();

        if (playCircleDemo != null)
        {
            Vector3 newYPosition = playCircleDemo.transform.position;
            newYPosition.y = value * 1f;
            playCircleDemo.transform.position = newYPosition;
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 1));
        }
    }

    public void IncreasePlayerCircleSize()
    {
        playCircleSlider[0].value += .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        StartCoroutine(ActivateCircleDemo());
    }

    public void DecreasePlayerCircleSize()
    {
        playCircleSlider[0].value -= .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        StartCoroutine(ActivateCircleDemo());
    }

    public void IncreasePlayerCircleHeight()
    {
        playCircleSlider[1].value += .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        StartCoroutine(ActivateCircleDemo());
    }

    public void DecreasePlayerCircleHeight()
    {
        playCircleSlider[1].value -= .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        StartCoroutine(ActivateCircleDemo());
    }

    IEnumerator ActivateCircleDemo()
    {
        demoOn = true;
        yield return new WaitForSeconds(5);
        demoOn = false;
    }

    public void DefaultSettings()
    {
        StartCoroutine(ResetBehavior());
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
    }

    public void OnResetEnter()
    {
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
    }

    private IEnumerator ResetBehavior()
    {
        isReset = true;
        yield return new WaitForSeconds(.2f);
        isReset = false;
        count = 0;
    }
}
