using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("------ Audio Clip --------")]
    public AudioClip death;
    public AudioClip wallCollision;
    public AudioClip hurt;
    public AudioClip coin;
    void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
