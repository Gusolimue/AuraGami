using UnityEngine;
using UnityEngine.Audio;
using System;

public enum eMixers { music, effects }
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //[NamedArray(typeof(eMixers))] public AudioMixerGroup[] mixers;
   // [NamedArray(typeof(eMixers))] public float[] volume = { 1f, 1f };
    //[NamedArray(typeof(eMixers))] public string[] strMixers = { "MusicVol", "EffectsVol" };

    private void Awake()
    {
        Instance = this;
    }

    public void SetMixerLevel (eMixers _mixer, float _soundLevel)
    {
       // mixers[(int)_mixer].audioMixer.SetFloat(strMixers[(int)_mixer], Mathf.Log10(_soundLevel) * 20f);
        //volume[(int)_mixer] = _soundLevel;
    }
}
