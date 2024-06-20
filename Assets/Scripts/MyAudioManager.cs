using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyAudioManager : MonoBehaviour
{
    public static MyAudioManager Instance;
    public AudioSource BackMenuMusic;
    public AudioSource BackGameMusic;
    public AudioSource StartButtonClick;
    public AudioSource MenuFadeSound;
    public AudioSource FoodColectSound;
    public AudioSource CountdownSound;
    public AudioSource StartSound;
    public AudioSource SmashSound;
    public AudioSource WinSound;
    public AudioSource ErrorSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundWithRandomPitch(AudioSource audioSource, float minPitch = 0.8f, float maxPitch = 1.2f)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
