using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    public float bpm;
    private float steps = 16;
    private int lastInterval;
    [System.NonSerialized] public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
            float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * GetIntervalLength(bpm)));
            CheckForNewInterval(sampledTime);
    }
    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * steps);
    }
    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            GameManager.Instance.ExecuteCounterIncreasing();
        }
    }
}