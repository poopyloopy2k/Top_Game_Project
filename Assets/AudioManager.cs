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
    public AudioClip background;
    public AudioClip wallCollision;
    public AudioClip hurt;
    public AudioClip coin;
    public AudioClip swordHit;
    public AudioClip spider;
    public AudioClip spiderDeath;
    public AudioClip spiderHurt;
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlayLoopingSFX(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.loop = true;
        SFXSource.Play();
    }
    public void StopSFX()
    {
        SFXSource.Stop();
    }
}
