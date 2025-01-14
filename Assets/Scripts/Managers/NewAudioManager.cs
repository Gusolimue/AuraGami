using UnityEngine;
using UnityEngine.Audio;

public enum eMixers { music, effects }
public class NewAudioManager : MonoBehaviour
{
    public static NewAudioManager Instance;

    /*[NamedArray(typeof(eMixers))]*/ public AudioMixerGroup[] mixers;
    /*[NamedArray(typeof(eMixers))]*/ public float[] volume = { 1f, 1f };
    /*[NamedArray(typeof(eMixers))]*/ public string[] strMixers = { "MusicVol", "EffectsVol" };

    [Space]
    [Header("Music Tracks")]
    [SerializeField] public AudioSource frontEndMusic;

    [Space]
    [Header("SFX")]
    [SerializeField] public AudioSource frontEndButtonSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMixerLevel (eMixers _mixer, float _soundLevel) // Allow for audio volume adjustments.
    {
        mixers[(int)_mixer].audioMixer.SetFloat(strMixers[(int)_mixer], Mathf.Log10(_soundLevel) * 20f);
        volume[(int)_mixer] = _soundLevel;
    }
}
