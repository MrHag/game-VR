using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    IEnumerator Transition(float time, int steps, AudioClip clip)
    {
        var stepTime = time / steps;
        var volume = audioSource.volume;
        var volumeStep = (volume - 0.0f) / steps;

        for (int step = 0; step < steps; step++)
        {
            volume -= volumeStep;
            yield return new WaitForSeconds(stepTime);
            audioSource.volume = volume;
        }

        audioSource.Stop();

        audioSource.clip = clip;
        audioSource.volume = 0.0f;

        audioSource.Play();

        volume = 0.0f;
        volumeStep = 1.0f / steps;

        for (int step = 0; step < steps; step++)
        {
            volume += volumeStep;
            yield return new WaitForSeconds(stepTime);
            audioSource.volume = volume;
        }

    }

    public void Play(AudioClip audioClip)
    {
        if (audioSource.isPlaying)
            StartCoroutine(Transition(6.0f, 60, audioClip));
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    void Start()
    {

    }
}
