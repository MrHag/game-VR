using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip SFX;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    private void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        SFXSource.Play();
    }
}
