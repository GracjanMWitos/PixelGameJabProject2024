using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    #region Assignments
    [Header("Assignments")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject moveIndicator;
    [Space]
    #endregion

    [Header("Beats Settings")]
    public bool isBeat;
    public bool isHalfbeat;
    [SerializeField] private float beatPerMinute = 70;
    [Range(0.1f,0.3f)] [SerializeField] private float mistakeMarginBeforeNote = 0.1f; // fraction of time between beat and halfbeat to give mistake margin before and after note
    [Range(0f, 0.099f)] [SerializeField] private float audioDelay = 0;
    Coroutine tactCoroutine;
    [Space]

    [Header("Debug")]
    [SerializeField] private float timeBetweenHalfbeats;
    [SerializeField] private float mistakeMarginTime;
    [SerializeField] private float mistakeTime;
    void Awake()
    {
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

        timeBetweenHalfbeats = 60 / beatPerMinute / 2; // 2 x maring + timeBetweenHalfbeat
        mistakeMarginTime = timeBetweenHalfbeats * mistakeMarginBeforeNote;
        mistakeTime = timeBetweenHalfbeats - mistakeMarginTime * 2;

        tactCoroutine = StartCoroutine(Tact());

    }


    private IEnumerator Tact()
    {
        BeatReactionTimeStart();                        //From now player can make boost shot and move
        yield return new WaitForSeconds(mistakeMarginTime - audioDelay); //Time before metronom sound
        OnBeat();                                       //Make metronom sound and show beat indicator
        yield return new WaitForSeconds(mistakeMarginTime + audioDelay); //TimeAfter metronom sound
        BeatReactionTimeEnd();                          //Fromm now player cannot move and make boost shot

        yield return new WaitForSeconds(mistakeTime);   //Time between beat margin's end and halfbeat margin's start

        HalfbeatReactionTimeStart();                    //From now player can make normal shot without breaking combo and make halfbeat check from skill beats combination
        yield return new WaitForSeconds(mistakeMarginTime - audioDelay); //Time before halfbeat
        OnHalfbeat();                                   //Sgow halfbeat indicator
        yield return new WaitForSeconds(mistakeMarginTime + audioDelay); //Time after halfbeat
        HalfbeatReactionTimeEnd();                      // From now player cannot make normal shot without breaking combo and make halfbeat check from skill beats combination

        yield return new WaitForSeconds(mistakeTime);   //Time between halfbeat margin's end and next cycle's beat margin's start
        tactCoroutine = StartCoroutine(Tact());
    }
    private IEnumerator IndicatorDelay()
    {
        yield return new WaitForSeconds(audioDelay);
        moveIndicator.SetActive(false);
    }
    private void BeatReactionTimeStart()
    {
        isBeat = true;
        moveIndicator.SetActive(true);
        
    }
    private void BeatReactionTimeEnd()
    {
        isBeat = false;
    }
    private void HalfbeatReactionTimeStart()
    {
        isHalfbeat = true;
    }
    private void HalfbeatReactionTimeEnd()
    {
        isHalfbeat = false;
    }
    private void OnBeat()
    {
        audioManager.PlayMetronomBeat();
        StartCoroutine(IndicatorDelay());
    }
    private void OnHalfbeat()
    {
        
    }
}