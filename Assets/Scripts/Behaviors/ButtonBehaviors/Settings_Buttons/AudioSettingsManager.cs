using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField] public GameObject audioConstellationOn;
    [SerializeField] public GameObject audioConstellationOff;

    [Header("Master Audio Slider")]
    [SerializeField] public Slider masterAudioSlider;
    public float masterAudioSliderValue;

    private void Awake()
    {
        audioConstellationOn.SetActive(false);
        audioConstellationOff.SetActive(true);

        masterAudioSlider.value = PlayerPrefs.GetFloat("save", masterAudioSliderValue);
        masterAudioSlider.onValueChanged.AddListener(MasterVolumeSlider);
    }
    public void OnAudioButtonEnter()
    {
        audioConstellationOn.SetActive(true);
        audioConstellationOff.SetActive(false);
        Debug.Log("Enter");
    }

    public void OnAudioButtonExit()
    {
        audioConstellationOn.SetActive(false);
        audioConstellationOff.SetActive(true);
        Debug.Log("Exit");
    }

    /*public void MasterVolumeSlider()
    {
        AudioManager.Instance.Master.setVolume(AudioManager.Instance.masterVolume);
    }*/

    public void MasterVolumeSlider(float value)
    {
        masterAudioSliderValue = value;
        PlayerPrefs.SetFloat("save", masterAudioSliderValue);
        PlayerPrefs.Save();
        

    }
}
