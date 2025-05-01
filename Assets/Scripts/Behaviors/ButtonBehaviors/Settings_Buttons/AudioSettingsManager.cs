using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField] public GameObject audioConstellationOn;
    [SerializeField] public GameObject audioConstellationOff;

    [Header("Audio Sliders")]
    [SerializeField] public Slider masterAudioSlider;
    private float masterAudioSliderValue;

    [SerializeField] public Slider musicAudioSlider;
    private float musicAudioSliderValue;

    [SerializeField] public Slider sfxAudioSlider;
    private float sfxAudioSliderValue;


    private void Awake()
    {
        audioConstellationOn.SetActive(false);
        audioConstellationOff.SetActive(true);

        masterAudioSlider.value = PlayerPrefs.GetFloat("save", masterAudioSliderValue);
        masterAudioSlider.onValueChanged.AddListener(MasterVolumeSlider);

        musicAudioSlider.value = PlayerPrefs.GetFloat("save", musicAudioSliderValue);
        musicAudioSlider.onValueChanged.AddListener(MusicVolumeSlider);

        sfxAudioSlider.value = PlayerPrefs.GetFloat("save", musicAudioSliderValue);
        sfxAudioSlider.onValueChanged.AddListener(SFXVolumeSlider);
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

    public void MasterVolumeSlider(float value)
    {
        masterAudioSliderValue = value;
        PlayerPrefs.SetFloat("save", masterAudioSliderValue);
        PlayerPrefs.Save();
        AudioManager.Instance.SetVolume(eBus.Master, PlayerPrefs.GetFloat("save"));
    }

    public void MusicVolumeSlider(float value)
    {
        musicAudioSliderValue = value;
        PlayerPrefs.SetFloat("save", musicAudioSliderValue);
        PlayerPrefs.Save();
        AudioManager.Instance.SetVolume(eBus.Music, PlayerPrefs.GetFloat("save"));
    }
    public void SFXVolumeSlider(float value)
    {
        sfxAudioSliderValue = value;
        PlayerPrefs.SetFloat("save", sfxAudioSliderValue);
        PlayerPrefs.Save();
        AudioManager.Instance.SetVolume(eBus.SFX, PlayerPrefs.GetFloat("save"));
    }

}
