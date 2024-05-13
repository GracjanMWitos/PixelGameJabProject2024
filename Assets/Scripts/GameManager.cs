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
    //Assigning via inspector
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject moveIndicator;

    //Assigning via code
    [HideInInspector] public GameObject player;
    [HideInInspector] public GridTile currentPlayerTile;
    [Space]

    #endregion

    [Header("Beats Settings")]
    public bool isBeat;
    public bool isHalfbeat;
    public float beatPerMinute = 70;
    [Range(0.2f, 0.3f)] [SerializeField] private float mistakeMarginBeforeNote = 0.1f; // fraction of time between beat and halfbeat to give mistake margin before and after note
    [Range(0f, 0.2f)] [SerializeField] private float indicatorDelay = 0;
    [Range(0f, 0.2f)] [SerializeField] private float audioDelay = 0;
    Coroutine tactCoroutine;
    [Space]

    [HideInInspector] public float timeBetweenHalfbeats;
    private float mistakeMarginTime;
    private float mistakeTime;
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
    }
    public void StartLevel()
    {
        moveIndicator = AttachMoveIndicatorToPlayer();
        #region Calculation to start beat
        timeBetweenHalfbeats = 60 / beatPerMinute / 2; // 2 x maring + timeBetweenHalfbeat
        mistakeMarginTime = timeBetweenHalfbeats * mistakeMarginBeforeNote;
        mistakeTime = timeBetweenHalfbeats - mistakeMarginTime * 2;
        tactCoroutine = StartCoroutine(Tact());
        #endregion
    }

    private GameObject AttachMoveIndicatorToPlayer()
    {
        return Instantiate(moveIndicator, player.transform.position, Quaternion.identity, player.transform);
    }

    #region Beat looping and beat actions
    private IEnumerator Tact()
    {
        BeatReactionTimeStart();                        //From now player can make boost shot and move
        yield return new WaitForSeconds(mistakeMarginTime - DelayScaling(indicatorDelay)); //Time before metronom sound
        OnBeat();                                       //Make metronom sound and show beat indicator
        yield return new WaitForSeconds(mistakeMarginTime + DelayScaling(indicatorDelay)); //TimeAfter metronom sound
        BeatReactionTimeEnd();                          //Fromm now player cannot move and make boost shot
        //Ater player reaction time are enemies moves
        EnemyTurn();

        yield return new WaitForSeconds(mistakeTime);   //Time between beat margin's end and halfbeat margin's start

        HalfbeatReactionTimeStart();                    //From now player can make normal shot without breaking combo and make halfbeat check from skill beats combination
        yield return new WaitForSeconds(mistakeMarginTime - DelayScaling(indicatorDelay)); //Time before halfbeat
        OnHalfbeat();                                   //Sgow halfbeat indicator
        yield return new WaitForSeconds(mistakeMarginTime + DelayScaling(indicatorDelay)); //Time after halfbeat
        HalfbeatReactionTimeEnd();                      // From now player cannot make normal shot without breaking combo and make halfbeat check from skill beats combination

        yield return new WaitForSeconds(mistakeTime);   //Time between halfbeat margin's end and next cycle's beat margin's start
        tactCoroutine = StartCoroutine(Tact());
    }
    private float DelayScaling(float delay)
    {
        delay *= timeBetweenHalfbeats;
        return delay;
    }
    private IEnumerator BeatAudioDelay()
    {
        yield return new WaitForSeconds(DelayScaling(audioDelay));
        audioManager.PlayMetronomBeat();

    }
    private void BeatReactionTimeStart()
    {
        isBeat = true;
        moveIndicator.SetActive(true);
        
    }
    
    private void BeatReactionTimeEnd()
    {
        isBeat = false;
        moveIndicator.SetActive(false);
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
        StartCoroutine(BeatAudioDelay());
    }
        private void OnHalfbeat()
    {
        
    }
    private void EnemyTurn()
    {
        if (EnemiesManager.Instance.enemiesArray.Length > 0)
        {
            EnemiesManager.Instance.ExecuteEnemiesActions();

        }
    }
    #endregion


}
