using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("SFX")]
    public AudioClip fishingFail;
    public AudioClip fishingSuccess;
    public AudioClip playerDeath;

    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CreateAudioSource("SFX", false);
    }

    private void CreateAudioSource(string name, bool shouldLoop = false)
    {
        GameObject go = new GameObject(name + "AudioSource");
        go.transform.SetParent(transform);
        AudioSource source = go.AddComponent<AudioSource>();
        source.loop = shouldLoop;
        audioSources[name] = source;
    }

    public void PlayFishingFail()
    {
        PlaySFX(fishingFail, "SFX");
    }

    public void PlayFishingSuccess()
    {
        PlaySFX(fishingSuccess, "SFX");
    }

    public void PlayPlayerDeath()
    {
        PlaySFX(playerDeath, "SFX");
    }
    
    private void PlaySFX(AudioClip clip, string channel = "SFX")
    {
        if(clip != null && audioSources.ContainsKey(channel))
        {
            audioSources[channel].PlayOneShot(clip);
        }
    }
}
