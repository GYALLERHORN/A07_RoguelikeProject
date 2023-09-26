using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioMixerGroup type, AudioClip clip, float minPitch, float maxPitch)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        _audioSource.outputAudioMixerGroup = type;
        _audioSource.clip = clip;
        _audioSource.pitch = Random.Range(minPitch, maxPitch);
        _audioSource.Play();

        Invoke("Disable", clip.length + 1);
    }

    public void Play(SoundManager.SoundData data)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        _audioSource.outputAudioMixerGroup = null;
        _audioSource.clip = data.Clip;
        _audioSource.volume = data.Volume;
        _audioSource.pitch = 1f + Random.Range(data.MinPitch, data.MaxPitch);
        transform.position = data.Position;
        _audioSource.Play();

        Invoke("Disable", data.Clip.length + 1);
    }

    public void Disable()
    {
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}
