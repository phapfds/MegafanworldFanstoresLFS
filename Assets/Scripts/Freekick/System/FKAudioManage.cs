using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum FKAudioType
{
    background,
    pip,
    kick,
    goal,
    fail,
}

[Serializable]
public class FKAudio
{
    public FKAudioType type;
    public AudioClip audioSource;
}

public class FKAudioManage : MonoBehaviour
{
    public static FKAudioManage Ins { get; private set; }
    [Header("Audio Sorce")]
    private Dictionary<FKAudioType, AudioClip> keyValuePairs = new Dictionary<FKAudioType, AudioClip>();
    [SerializeField] List<FKAudio> fKAudios;

    private void Awake()
    {
        Ins = this;
        foreach (FKAudio fKAudio in fKAudios)
        {
            keyValuePairs.Add(fKAudio.type, fKAudio.audioSource);
        }
    }

    public void PlaySound(FKAudioType fKAudioType)
    {
        GameObject sound = new GameObject("Sound");
        AudioSource audioSource = sound.AddComponent<AudioSource>();
        audioSource.PlayOneShot(keyValuePairs[fKAudioType]);
        Destroy(sound, keyValuePairs[fKAudioType].length);
    }

}
