using System.Collections;
using UnityEngine;
using System;

[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(-3f, 3f)]
    public float pitch = 1f;
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;
    public bool loop = false;
    public bool playOnAwake = false;
    [HideInInspector]
    public AudioSource aS;
}

public class SoundManager : MonoBehaviour
{
    public Audio[] soundFX;

    void Awake()
    {
        foreach( Audio aud in soundFX)
        {
            aud.aS = gameObject.AddComponent<AudioSource>();
            aud.aS.clip = aud.clip;
            aud.aS.volume = aud.volume;
            aud.aS.loop = aud.loop;
            aud.aS.pitch = aud.pitch;
            aud.aS.playOnAwake = aud.playOnAwake;
        }
    }

    public void PlaySound(string name)
    {
        Audio aud = Array.Find(soundFX, Audio => Audio.name == name);
        if(aud == null)
        {
            Debug.LogError("Audio : " + aud.name + " not found to play!");
            return;
        }

        aud.aS.volume = aud.volume * (1 + UnityEngine.Random.Range(-aud.randomVolume / 2f, aud.randomVolume / 2f));
        aud.aS.pitch = aud.pitch * (1 + UnityEngine.Random.Range(-aud.randomPitch / 2f, aud.randomPitch / 2f));

        if(!aud.aS.isPlaying)
        {
            aud.aS.Play();
        }
    }

    public void Mute(string name)
    {
        StartCoroutine(MuteSound(name));
    }

    IEnumerator MuteSound(string name)
    {
        Audio aud = Array.Find(soundFX, Audio => Audio.name == name);
        if (aud == null)
        {
            Debug.LogError("Audio : " + aud.name + " not found to mute !");
            yield break;
        }

        float totalFadingTime = 0.5f;
        float currentFadingTime = 0f;

        while(aud.aS.volume > 0f)
        {
            currentFadingTime += Time.deltaTime;
            aud.aS.volume = Mathf.Lerp(1f, 0f, currentFadingTime / totalFadingTime);
            yield return null;
        }

        if(aud.aS.volume <= 0.01f)
        {
            StopSound(name);
        }
    }

    public void StopSound(string name)
    {
        Audio aud = Array.Find(soundFX, Audio => Audio.name == name);
        if (aud == null)
        {
            Debug.LogError("Audio : " + aud.name + " not found to stop !");
            return;
        }

        if(aud.aS.isPlaying)
        {
            aud.aS.Stop();
        }
    }
}
