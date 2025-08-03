using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    [Space]
    [Header("Master Vol")]
    [SerializeField] public Slider masterAudioSlider;
    private float masterAudioSliderValue;
    private float targetMasterValue;

    [Header("Music Vol")]
    [SerializeField] public Slider musicAudioSlider;
    private float musicAudioSliderValue;

    [Header("SFX Vol")]
    [SerializeField] public Slider sfxAudioSlider;
    private float sfxAudioSliderValue;
    [Space]
    [SerializeField] Image targetHandleSize;

    [Header("Slider Handle")]
    [SerializeField] Image[] sliderHandle;
    private float masterHandleSize = .035f;
    private float musicHandleSize = .035f;
    private float sfxHandleSize = .035f;

    [Header("Reset")]
    [SerializeField] Image resetFill;
    [SerializeField] Color[] fillColor;
    private float count;
    private bool isReset;

    private void Awake()
    {
        masterAudioSlider.onValueChanged.AddListener(MasterVolumeSlider);
        masterAudioSlider.value = PlayerPrefs.GetFloat("saveAll", .5f);
        //masterAudioSlider.OnDrag();

        masterHandleSize = PlayerPrefs.GetFloat("masterHandle");
        //sliderHandle[0].transform.localScale = new Vector3(masterHandleSize, masterHandleSize, masterHandleSize);
        musicAudioSlider.onValueChanged.AddListener(MusicVolumeSlider);
        musicAudioSlider.value = PlayerPrefs.GetFloat("saveMusic", .5f);

        sfxAudioSlider.onValueChanged.AddListener(SFXVolumeSlider);
        sfxAudioSlider.value = PlayerPrefs.GetFloat("saveSFX", .5f);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isReset) resetFill.color = Color.Lerp(resetFill.color, fillColor[0], count / 5);
        else resetFill.color = Color.Lerp(resetFill.color, fillColor[1], count / 5);

        //masterAudioSlider.value = Mathf.Lerp(masterAudioSlider.value, targetMasterValue, Time.deltaTime * 5f);

        if (Input.GetKeyDown(KeyCode.L)) IncreaseMasterVol();
        if (Input.GetKeyDown(KeyCode.K)) DecreaseMasterVol();
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
        PlayerPrefs.SetFloat("masterHandle", masterHandleSize);
        Debug.Log("Welp");

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
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
    }

    public void DecreaseMusicVol()
    {
        musicAudioSlider.value -= .1f;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
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

    public void DefaultSettings()
    {
        StartCoroutine(ResetBehavior());
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        masterAudioSlider.value = .5f;
        AudioManager.Instance.SetVolume(eBus.Master, PlayerPrefs.GetFloat("saveAll"));

        musicAudioSlider.value = .5f;
        AudioManager.Instance.SetVolume(eBus.Music, PlayerPrefs.GetFloat("saveMusic"));

        sfxAudioSlider.value = .5f;
        AudioManager.Instance.SetVolume(eBus.SFX, PlayerPrefs.GetFloat("saveSFX"));

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
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

    //public class CustomSlider : Slider
    //{
    //    public override void OnPointerDown(PointerEventData eventData)
    //    {
    //        base.OnPointerDown(eventData);


    //        // Both controllers vibrating is temporary behavior until I figure out how to target specifically which controller is currently selecting the slider
    //        HapticsManager.Instance.ToggleVibration(eSide.both, true, base.value);
    //    }

    //    public override void OnPointerUp(PointerEventData eventData)
    //    {
    //        base.OnPointerUp(eventData);

    //        // Both controllers disabling vibration is temporary behavior until I figure out how to target specifically which controller is currently deselecting the slider
    //        HapticsManager.Instance.ToggleVibration(eSide.both, false, base.value);
    //    }
    //}
}
