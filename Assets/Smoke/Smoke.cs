using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{

    public ParticleSystem particle;

    public List<Smoke> subSmokes;

    public Color baseColor;

    public bool continuousPlay;

    // Start is called before the first frame update

    private Coroutine coroutine;

    IEnumerator Play(float seconds)
    {
        if (!continuousPlay)
            particle.Play();
        yield return new WaitForSeconds(seconds);
        if (!continuousPlay)
            particle.Stop();
        else
            particle.startColor = baseColor;
    }

    public void Play(float seconds, Color color)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        particle.startColor = color;

        coroutine = StartCoroutine(Play(seconds));

        if (subSmokes != null)
            foreach (Smoke smoke in subSmokes)
            {
                smoke.Play(seconds, color);
            }
    }

    void Start()
    {
        particle.startColor = baseColor;
        if (continuousPlay)
            particle.Play();
    }

    void OnValidate()
    {
        particle.startColor = baseColor;
    }
}
