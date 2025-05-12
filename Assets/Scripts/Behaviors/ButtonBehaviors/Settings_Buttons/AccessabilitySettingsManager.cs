using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccessabilitySettingsManager : MonoBehaviour
{
    public static AccessabilitySettingsManager Instance;

    [Header("Player Circle/Slider")]
    [SerializeField] public Slider playCircleSlider;
    [SerializeField] Image playCircleDemo;
    [SerializeField] GameObject playerCircle;
    [SerializeField] Color[] colorChanges;
    public float playCircleSliderValue;
    private bool demoOn = false;

    private void Awake()
    {
        Instance = this;
       // playCircleSlider.value = 1f;

        playCircleSlider.value = PlayerPrefs.GetFloat("playCircleScale");
        playCircleSlider.onValueChanged.AddListener(ChangeSlider);
        playCircleDemo.color = colorChanges[1];

        Debug.Log(playCircleSlider.value);
    }

    private void Update()
    {
        if (demoOn) playCircleDemo.color = Color.Lerp(playCircleDemo.color, colorChanges[0], Time.deltaTime * 5);
        if (!demoOn) playCircleDemo.color = Color.Lerp(playCircleDemo.color, colorChanges[1], Time.deltaTime * 5);
    }

    public void ChangeSlider(float value)
    {
        Debug.Log("Saving slider value: " + value);

        playCircleSliderValue = value;
        PlayerPrefs.SetFloat("playCircleScale", value);
        PlayerPrefs.Save();

        if (playCircleDemo != null) playCircleDemo.transform.localScale = Vector3.one * value;
        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.SetScale(PlayerPrefs.GetFloat("playCircleScale"));

        }
    }

    public void IncreasePlayerCircleSize()
    {
        playCircleSlider.value += .1f;
        StartCoroutine(ActivateCircleDemo());
    }

    public void DecreasePlayerCircleSize()
    {
        playCircleSlider.value -= .1f;
        StartCoroutine(ActivateCircleDemo());
    }

    IEnumerator ActivateCircleDemo()
    {
        demoOn = true;
        yield return new WaitForSeconds(5);
        demoOn = false;
    }
}
