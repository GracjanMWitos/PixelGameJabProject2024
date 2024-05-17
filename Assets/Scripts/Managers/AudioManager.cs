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
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    public List<SoundsGroup> soundsGroup = new List<SoundsGroup>();
    private AudioSource audioSource;
    public AudioInBioms audioInCurrentBiom;
    [SerializeField] private AudioClip beatSound;
    public SoundsGroup currentSoundGroup;

    public AudioClip mainTrack;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetSoundsGroup();

        #region Instance check
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
        mainTrack = soundsGroup[0].ambient;
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
    public void PlayKick()
    {
        audioSource.PlayOneShot(soundsGroup[0].moveSound);
    }
    public void PlayHiHat()
    {
        audioSource.PlayOneShot(soundsGroup[0].skillSound1);
    }
    public void PlayMetronomBeat()
    {
        audioSource.PlayOneShot(soundsGroup[0].ambient);
    }
}
