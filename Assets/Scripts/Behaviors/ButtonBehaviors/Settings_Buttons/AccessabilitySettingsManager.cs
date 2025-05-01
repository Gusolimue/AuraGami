using UnityEngine;
using UnityEngine.UI;

public class AccessabilitySettingsManager : MonoBehaviour
{
    public static AccessabilitySettingsManager Instance;

    [Header("Accessibility Button Elements")]
    [SerializeField] public GameObject accessabilityConstellationOn;
    [SerializeField] public GameObject accessabilityConstellationOff;

    [Header("Player Circle/Slider")]
    [SerializeField] public Slider playCircleSlider;
    [SerializeField] GameObject playCircleDemo;
    public float playCircleSliderValue;

    private void Awake()
    {
        Instance = this;
       // playCircleSlider.value = 1f;

        accessabilityConstellationOn.SetActive(false);
        accessabilityConstellationOff.SetActive(true);

        playCircleSlider.value = PlayerPrefs.GetFloat("save", playCircleSliderValue);
        playCircleSlider.onValueChanged.AddListener(ChangeSlider);

        Debug.Log(playCircleSlider.value);
    }

    public void OnAccessabilityButtonEnter()
    {
        accessabilityConstellationOn.SetActive(true);
        accessabilityConstellationOff.SetActive(false);
    }

    public void OnAccessabilityButtonExit()
    {
        accessabilityConstellationOn.SetActive(false);
        accessabilityConstellationOff.SetActive(true);
    }

    public void ChangeSlider(float value)
    {
        Debug.Log("Saving slider value: " + value);

        playCircleSliderValue = value;
        PlayerPrefs.SetFloat("save", playCircleSliderValue);
        PlayerPrefs.Save();

        if (playCircleDemo != null) playCircleDemo.transform.localScale = new Vector3(value, value, value);
        PlayerPrefs.SetFloat("playCircleScaleX", playCircleDemo.transform.localScale.x);
        PlayerPrefs.SetFloat("playCircleScaleY", playCircleDemo.transform.localScale.y);
        PlayerPrefs.SetFloat("playCircleScaleZ", playCircleDemo.transform.localScale.z);
        PlayerPrefs.Save();
    }
}
