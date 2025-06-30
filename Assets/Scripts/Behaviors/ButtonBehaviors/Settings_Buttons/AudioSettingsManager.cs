using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    [Space]
    [Header("Master Vol")]
    [SerializeField] public Slider masterAudioSlider;
    [SerializeField] Image masterHandle; 
    private float masterAudioSliderValue;

    [Header("Music Vol")]
    [SerializeField] public Slider musicAudioSlider;
    private float musicAudioSliderValue;

    [Header("SFX Vol")]
    [SerializeField] public Slider sfxAudioSlider;
    private float sfxAudioSliderValue;
    [Space]
    [SerializeField] Image targetHandleSize;

    [Header("Reset")]
    [SerializeField] Image resetFill;
    [SerializeField] Color[] fillColor;
    private float count;
    private bool isReset;

    private void Awake()
    {
        masterAudioSlider.value = PlayerPrefs.GetFloat("saveAll", masterAudioSliderValue);
        masterAudioSlider.onValueChanged.AddListener(MasterVolumeSlider);

        musicAudioSlider.value = PlayerPrefs.GetFloat("saveMusic", musicAudioSliderValue);
        musicAudioSlider.onValueChanged.AddListener(MusicVolumeSlider);

        sfxAudioSlider.value = PlayerPrefs.GetFloat("saveSFX", musicAudioSliderValue);
        sfxAudioSlider.onValueChanged.AddListener(SFXVolumeSlider);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isReset) resetFill.color = Color.Lerp(resetFill.color, fillColor[0], count / 5);
        else resetFill.color = Color.Lerp(resetFill.color, fillColor[1], count / 5);
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
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
    }

    public void DecreaseMasterVol()
    {
        masterAudioSlider.value -= .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
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
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
    }

    public void DecreaseSFXVol()
    {
        sfxAudioSlider.value -= .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
    }

    public void DefaultSettings()
    {
        StartCoroutine(ResetBehavior());
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        masterAudioSlider.value = 1;
        AudioManager.Instance.SetVolume(eBus.Master, PlayerPrefs.GetFloat("saveAll"));

        musicAudioSlider.value = 1;
        AudioManager.Instance.SetVolume(eBus.Music, PlayerPrefs.GetFloat("saveMusic"));

        sfxAudioSlider.value = 1;
        AudioManager.Instance.SetVolume(eBus.SFX, PlayerPrefs.GetFloat("saveSFX"));
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
