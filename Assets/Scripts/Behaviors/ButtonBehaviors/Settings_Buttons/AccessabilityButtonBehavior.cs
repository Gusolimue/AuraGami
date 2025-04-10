using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AccessabilityButtonBehavior : MonoBehaviour
{
    [SerializeField] public GameObject accessabilityConstellationOn;
    [SerializeField] public GameObject accessabilityConstellationOff;

    //[SerializeField] UnityEngine.UI.Slider playCircleSlider;
    public float playCircleSliderValue;

    private void Awake()
    {
        accessabilityConstellationOn.SetActive(false);
        accessabilityConstellationOff.SetActive(true);

        //PlayerPrefs()

       // playCircleSliderValue = playCircleSlider.value;
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

    public void playCircleSliderBehavior()
    {
        //playCircleSlider.transform.localScale = new Vector3(scale, scale, scale);
    }
}
