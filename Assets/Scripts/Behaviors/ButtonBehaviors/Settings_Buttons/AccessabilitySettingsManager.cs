using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AccessabilitySettingsManager : MonoBehaviour
{
    public static AccessabilitySettingsManager Instance;

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

    private void Awake()
    {
        Instance = this;

        playCircleSlider[0].value = PlayerPrefs.GetFloat("playCircleScale");
        playCircleSlider[0].onValueChanged.AddListener(ChangeSlider);
        playCircleDemo.color = colorChanges[1];

        playCircleSlider[1].value = PlayerPrefs.GetFloat("playCircleHeight");
        playCircleSlider[1].onValueChanged.AddListener(ChangePlayerCircleHeightSlider);

        toggleNum = PlayerPrefs.GetInt("toggleCircle");
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
            AvatarManager.Instance.SetScale(PlayerPrefs.GetFloat("playCircleScale"));

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
    }

    public void IncreasePlayerCircleSize()
    {
        playCircleSlider[0].value += .1f;
        StartCoroutine(ActivateCircleDemo());
    }

    public void DecreasePlayerCircleSize()
    {
        playCircleSlider[0].value -= .1f;
        StartCoroutine(ActivateCircleDemo());
    }

    public void IncreasePlayerCircleHeight()
    {
        playCircleSlider[1].value += .1f;
        StartCoroutine(ActivateCircleDemo());
    }

    public void DecreasePlayerCircleHeight()
    {
        playCircleSlider[1].value -= .1f;
        StartCoroutine(ActivateCircleDemo());
    }

    IEnumerator ActivateCircleDemo()
    {
        demoOn = true;
        yield return new WaitForSeconds(5);
        demoOn = false;
    }
}
