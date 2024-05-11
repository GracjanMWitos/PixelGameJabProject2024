using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundsGroup
{
    public AudioInBioms audioInBiom;
    public AudioClip moveSound;
    public AudioClip lightAttackSound;
    public AudioClip heavyAttackSound;
    public AudioClip skillSound1;
    public AudioClip skillSound2;
    public AudioClip ambient;
}
    public enum AudioInBioms { First }

public class AudioManager : MonoBehaviour
{
    public List<SoundsGroup> soundsGroup = new List<SoundsGroup>();
    private AudioSource audioSource;
    public AudioInBioms audioInCurrentBiom;
    [SerializeField] private AudioClip beatSound;
    public SoundsGroup currentSoundGroup;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetSoundsGroup();



    }
    public void SetSoundsGroup()
    {
        foreach (SoundsGroup soundsGroup in soundsGroup)
        {
            if (soundsGroup.audioInBiom == audioInCurrentBiom)
            {
                currentSoundGroup = soundsGroup;
            }
        }

    }
    public void PlayMetronomBeat()
    {
        audioSource.PlayOneShot(beatSound);
    }
}
