using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // [Header("---------- Audio Source ----------")]
    public AudioSource musicSource;
    // public AudioSource SFXSource;

    // [Header("---------- Audio Clip ----------")]
    // public AudioClip background;
    // public AudioClip SFX;

    // private void Play()
    // {

    // }


    void Start()
    {
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}
