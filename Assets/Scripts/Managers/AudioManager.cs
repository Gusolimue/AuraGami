using UnityEngine;
using UnityEngine.Audio;
using System;

//Quick and Temporary script to play a song as the evel starts
public class AudioManager : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public AudioClip[] songs;
    [Header("Variables to Set")]
    public AudioSource audioSource;
    [Header("Variables to Call")]
    public static AudioManager Instance;
    private void Awake()
    {
        Instance = this;
        playSong(songs[0]);
    }
    //Stops the playing track and plays the input song
    public void playSong(AudioClip _song)
    {
        audioSource.Stop();
        audioSource.clip = _song;
        audioSource.Play();
    }
}
