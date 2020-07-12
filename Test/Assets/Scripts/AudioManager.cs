using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float musicVolume = 1f;

    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private static AudioManager privateInstance;

    public static AudioManager instance
    {
        get
        {
            if (privateInstance == null)
            {
                //if there is no instance of AudioManager, create one
                privateInstance = FindObjectOfType<AudioManager>();
                if (privateInstance == null)
                {
                    privateInstance = new GameObject("Created AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return privateInstance;
        }
        private set
        {
            privateInstance = value;
        }
    }

    //need 2 music sources to transition between scenes
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    bool firstMusicSourceIsPlaying;

    void Awake()
    {
        //make sure we don#t destroy this instance
        DontDestroyOnLoad(this.gameObject);

        //create audiosource components
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        //make sure the music tracks are looping
        musicSource.loop = true;
        musicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource;

        //check which music source is playing
        if (firstMusicSourceIsPlaying)
        {
            activeSource = musicSource;
        }
        else
        {
            activeSource = musicSource2;
        }

        activeSource.clip = musicClip;
        activeSource.volume = musicVolume;
        activeSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.volume = sfxVolume;
        sfxSource.PlayOneShot(sfxClip);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
        musicSource2.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = volume;
    }

    //use to ensure the sfx source only plays one clip i.e. for volume slider
    public void PlaySoundOnce(AudioClip sfxClip)
    {
        if (!sfxSource.isPlaying)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }
}
