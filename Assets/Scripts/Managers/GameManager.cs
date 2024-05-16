using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    #region Assignments
    [Header("Assignments")]
    //Assigning via inspector
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject moveIndicator;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private CinemachineVirtualCamera cinemachine;

    //Assigning via code
    [HideInInspector] public GameObject player;
    [HideInInspector] public GridTile currentPlayerTile;
    private PlayerController playerController;

    public bool start;
    [Space]

    #endregion

    [Header("Beats Settings")]
    public bool isBeat;
    public bool isHalfbeat;
    public float beatPerMinute = 70;

    [Range(1f,3f)][SerializeField] private float marginAsFractionOfTime = 2f; // fraction of time between beat and halfbeat to give mistake margin before and after note
    private float marginTime;
    [Range(0.125f, 0.875f)] [SerializeField] private float reactionCheckBalance = 0.75f;
    private float timeOfMarginBeforeNote;
    private float timeOfMarginAfterNote;

    [SerializeField] private float loopingDelay = 0;
    private float loopingDelayPerBeat;
    public float audioDelay;

    [Space]

    public float timeBetweenHalfbeats;
    private void Update()
    {
        if (start)
        {
            StartLevel();
            start = false;
        }
    }
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
    
    #region Level Starting
    public void StartLevel()
    {
        #region Spawn player and set his attachment

        player = playerSpawner.SpawnPlayer();
        playerController = player.GetComponent<PlayerController>();
        currentPlayerTile = playerController.GetPlayerTile();
        moveIndicator = AttachMoveIndicatorToPlayer();
        cinemachine.Follow = player.transform;
        EnemiesManager.Instance.RefreshEnemiesList();
        #endregion

        #region Calculation for loops

        timeBetweenHalfbeats = (60/ beatPerMinute)/2 ;
        //margins
        marginTime = timeBetweenHalfbeats / marginAsFractionOfTime;
        timeOfMarginBeforeNote = marginTime * reactionCheckBalance;
        timeOfMarginAfterNote = marginTime * (1 - reactionCheckBalance);

        loopingDelayPerBeat = loopingDelay / 16;
        #endregion

        StartCoroutine(Loop());

    }
    private GameObject AttachMoveIndicatorToPlayer()
    {
        return Instantiate(moveIndicator, player.transform.position, Quaternion.identity, player.transform);
    }
    #endregion
    public void GameOver()
    {
        Debug.Log("Œe ci zdech³o, przykro mi");
    }

    private IEnumerator DelayAudio()
    {
        yield return new WaitForSecondsRealtime(audioDelay);
        audioManager.PlayMetronomBeat();
    }

    private IEnumerator Loop()
    {

        #region 1 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        StartCoroutine(DelayAudio());

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();                          

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 2 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 3 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 4 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 5 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 6 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 7 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 8 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 9 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 10 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 11 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 12 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 13 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 14 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 15 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        #region 16 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime - loopingDelayPerBeat);
        #endregion

        StartCoroutine(Loop());
    }

    #region Reactions Methods
    private void BeatReactionTimeStart()
    {
        isBeat = true;
        moveIndicator.SetActive(true);
    }
    private void BeatReactionTimeEnd()
    {
        isBeat = false;
        playerController.canMove = true;
        playerController.canShot = true;
        moveIndicator.SetActive(false);
    }
    private void HalfbeatReactionTimeStart()
    {
        isHalfbeat = true;
    }
    private void HalfbeatReactionTimeEnd()
    {
        isHalfbeat = false;
        playerController.canShot = true;
    }
    #endregion
    private void OnBeat()
    {
        
        GUIManager.Instance.MoveNotesToNextTarget();
    }
    private void OnHalfbeat()
    {
        GUIManager.Instance.MoveNotesToNextTarget();

    }
    private void EnemyTurn()
    {
        if (EnemiesManager.Instance.enemyControllers.Count > 0)
        {
            EnemiesManager.Instance.ExecuteEnemiesActions();

        }
    }


}
