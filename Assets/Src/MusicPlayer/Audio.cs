using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;

    public Coroutine coroutine;

    void Start()
    {

    }

    public void Play(float seconds = 0)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        audioSource.Play();

        if (seconds > 0.0f)
        {
            coroutine = StartCoroutine(StopAfter(seconds));
        }
    }

    public IEnumerator StopAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Stop();
    }

    public void Stop()
    {
        audioSource.Stop();
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
