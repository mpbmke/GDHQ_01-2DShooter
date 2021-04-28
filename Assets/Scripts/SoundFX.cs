using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip _explosionSFX;
    [SerializeField] AudioClip _laserSFX;
    [SerializeField] AudioClip _powerUpSFX;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("SoundFX AudioSource is null");
        }
    }

    public void ExplosionAudio()
    {
        _audioSource.PlayOneShot(_explosionSFX);
    }

    public void LaserAudio()
    {
        _audioSource.PlayOneShot(_laserSFX);
    }

    public void PoweUpAudio()
    {
        _audioSource.PlayOneShot(_powerUpSFX);
    }
}
