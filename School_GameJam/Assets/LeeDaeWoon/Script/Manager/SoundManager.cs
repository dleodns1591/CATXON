using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum SoundType
{
    SFX,
    BGM,
    END
}
public class AudioSourceClass
{
    public AudioSource audioSource;
    public float audioVolume;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    public Dictionary<SoundType, AudioSourceClass> audioSourceClasses = new Dictionary<SoundType, AudioSourceClass>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/");
        foreach (AudioClip clip in clips)
        {
            audioClips[clip.name] = clip;
        }

        string[] enumNames = Enum.GetNames(typeof(SoundType));
        for (int i = 0; i < (int)SoundType.END; i++)
        {
            GameObject AudioSourceObj = new GameObject(enumNames[i]);
            AudioSourceObj.transform.SetParent(transform);
            // TODO : »ý¼ºÀÚ
            AudioSourceClass sourceClass
                = new AudioSourceClass { audioSource = AudioSourceObj.AddComponent<AudioSource>(), audioVolume = 0.5f };
            audioSourceClasses[(SoundType)i] = sourceClass;
        }


        audioSourceClasses[SoundType.BGM].audioSource.loop = true;
    }

    public AudioClip PlaySoundClip(string clipName, SoundType type, float volume = 0.5f, float pitch = 1f)
    {
        AudioClip clip = audioClips[clipName];
        audioSourceClasses[type].audioSource.pitch = pitch;

        float curVolume = volume * audioSourceClasses[type].audioVolume;
        if (type == SoundType.BGM)
        {
            audioSourceClasses[SoundType.BGM].audioSource.clip = clip;
            audioSourceClasses[SoundType.BGM].audioSource.volume = curVolume;
            audioSourceClasses[SoundType.BGM].audioSource.Play();
        }
        else
        {
            audioSourceClasses[type].audioSource.PlayOneShot(clip, curVolume);
        }

        return clip;
    }

}
