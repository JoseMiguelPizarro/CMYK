using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource audioSource;
    public AudioClip highspeedClip;
    public AudioClip normalClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void PlayNormalClip()
    {
        audioSource.clip = normalClip;
        audioSource.Play();
    }

    public void PlayHighSpeedMusic()
    {
        audioSource.clip = highspeedClip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Pause();
    }
}
