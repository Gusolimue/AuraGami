using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{


    [Header("Audio Sliders")]
    [SerializeField] public Slider masterAudioSlider;
    private float masterAudioSliderValue;

    [SerializeField] public Slider musicAudioSlider;
    private float musicAudioSliderValue;

    [SerializeField] public Slider sfxAudioSlider;
    private float sfxAudioSliderValue;


    private void Awake()
    {
        masterAudioSlider.value = PlayerPrefs.GetFloat("saveAll", masterAudioSliderValue);
        masterAudioSlider.onValueChanged.AddListener(MasterVolumeSlider);

        musicAudioSlider.value = PlayerPrefs.GetFloat("saveMusic", musicAudioSliderValue);
        musicAudioSlider.onValueChanged.AddListener(MusicVolumeSlider);

        sfxAudioSlider.value = PlayerPrefs.GetFloat("saveSFX", musicAudioSliderValue);
        sfxAudioSlider.onValueChanged.AddListener(SFXVolumeSlider);
    }

    public void MasterVolumeSlider(float value)
    {
        masterAudioSliderValue = value;
        PlayerPrefs.SetFloat("saveAll", masterAudioSliderValue);
        PlayerPrefs.Save();
        AudioManager.Instance.SetVolume(eBus.Master, PlayerPrefs.GetFloat("saveAll"));
    }

    public void IncreaseMasterVol()
    {
        masterAudioSlider.value += .1f;
    }

    public void DecreaseMasterVol()
    {
        masterAudioSlider.value -= .1f;
    }

    public void MusicVolumeSlider(float value)
    {
        musicAudioSliderValue = value;
        PlayerPrefs.SetFloat("saveMusic", musicAudioSliderValue);
        PlayerPrefs.Save();
        AudioManager.Instance.SetVolume(eBus.Music, PlayerPrefs.GetFloat("saveMusic"));
    }

    public void IncreaseMusicVol()
    {
        musicAudioSlider.value += .1f;
    }

    public void DecreaseMusicVol()
    {
        musicAudioSlider.value -= .1f;
    }

    public void SFXVolumeSlider(float value)
    {
        sfxAudioSliderValue = value;
        PlayerPrefs.SetFloat("saveSFX", sfxAudioSliderValue);
        PlayerPrefs.Save();
        AudioManager.Instance.SetVolume(eBus.SFX, PlayerPrefs.GetFloat("saveSFX"));
    }

    public void IncreaseSFXVol()
    {
        sfxAudioSlider.value += .1f;
    }

    public void DecreaseSFXVol()
    {
        sfxAudioSlider.value -= .1f;
    }
}
