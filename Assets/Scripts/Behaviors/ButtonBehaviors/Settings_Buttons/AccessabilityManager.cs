using UnityEngine;
using UnityEngine.UI;

public class AccessabilityManager : MonoBehaviour
{
    public static AccessabilityManager Instance;

    [Header("Accessibility Button Elements")]
    [SerializeField] public GameObject accessabilityConstellationOn;
    [SerializeField] public GameObject accessabilityConstellationOff;

    [Header("Player Circle Slider")]
    [SerializeField] public Slider playCircleSlider;
    public float playCircleSliderValue;

    private void Awake()
    {
        Instance = this;

        accessabilityConstellationOn.SetActive(false);
        accessabilityConstellationOff.SetActive(true);
    }

    private void Start()
    {
        playCircleSlider.value = PlayerPrefs.GetFloat("save", playCircleSliderValue);
        //playCircleSlider.value = playCircleSliderValue;
        playCircleSlider.onValueChanged.AddListener(ChangeSlider);
        Debug.Log("Slider Value Load: " + PlayerPrefs.GetFloat("save", playCircleSliderValue));
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
    }
}
