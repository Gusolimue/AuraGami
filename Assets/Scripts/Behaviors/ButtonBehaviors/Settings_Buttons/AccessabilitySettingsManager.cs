using UnityEngine;
using UnityEngine.UI;

public class AccessabilitySettingsManager : MonoBehaviour
{
    public static AccessabilitySettingsManager Instance;

    [Header("Player Circle/Slider")]
    [SerializeField] public Slider playCircleSlider;
    [SerializeField] GameObject playCircleDemo;
    public float playCircleSliderValue;

    private void Awake()
    {
        Instance = this;
       // playCircleSlider.value = 1f;

        playCircleSlider.value = PlayerPrefs.GetFloat("save", playCircleSliderValue);
        playCircleSlider.onValueChanged.AddListener(ChangeSlider);

        Debug.Log(playCircleSlider.value);
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
