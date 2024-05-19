using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Cinemachine;
using Debug = UnityEngine.Debug;

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
    [SerializeField] private LevelFinishingSequence levelFinishingSequence;

    //Assigning via code
    [HideInInspector] public GameObject player;
    [HideInInspector] public GridTile currentPlayerTile;
    private PlayerController playerController;
    [Space]

    #endregion

    [HideInInspector] public bool isBeat;
    [HideInInspector] public bool isHalfbeat;

    [Header("Beats Settings")]
    public float beatPerMinute = 70;
    [Range(1f,3f)][SerializeField] private float marginAsFractionOfTime = 2f; // fraction of time between beat and halfbeat to give mistake margin before and after note
    [Range(0.125f, 0.875f)] [SerializeField] private float reactionCheckBalance = 0.75f;

    [HideInInspector] public float timeBetweenHalfbeats;
    private float marginTime;
    private float timeOfMarginBeforeNote;
    private float timeOfMarginAfterNote;
    [Space]
    public float audioDelay;
    public float millisecondsOfLoopActivationSpeedUp;
    private float loopLenght = 9.6f;
    private Stopwatch timer = new Stopwatch();

    private void Update()
    {
        if (timer.Elapsed.TotalMilliseconds >= (loopLenght * 1000) - millisecondsOfLoopActivationSpeedUp)
        {
            Debug.Log(timer.Elapsed.TotalMilliseconds);
            StartCoroutine(Loop());
            timer.Reset();
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
    private void Start()
    {
        StartLevel();
    }
    #region Level Starting
    public void StartLevel()
    {
        #region Spawn player and set his attachment

        player = playerSpawner.SpawnPlayer();
        playerController = player.GetComponent<PlayerController>();
        currentPlayerTile = playerController.getGridTile.GetTile(player.transform.position);
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


        Debug.Log("Margin time " + marginTime);
        Debug.Log("Margin time before  " + timeOfMarginBeforeNote);
        Debug.Log("Margin time after " + timeOfMarginAfterNote);
        #endregion

        StartCoroutine(Loop());

    }
    private GameObject AttachMoveIndicatorToPlayer()
    {
        return Instantiate(moveIndicator, player.transform.position, Quaternion.identity, player.transform);
    }
    #endregion

    public void FinishLevel()
    {
        levelFinishingSequence.enabled = true;
    }

    public void GameOver()
    {
        GUIManager.Instance.DeathScreanActivation();
    }

    private IEnumerator DelayAudio()
    {
        yield return new WaitForSecondsRealtime(audioDelay);
        audioManager.PlayMetronomBeat();
        timer.Start();
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

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();                          

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 2 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 3 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 4 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 5 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 6 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 7 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 8 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 9 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 10 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 11 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 12 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 13 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 14 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 15 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion

        #region 16 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        EnemyTurn();
        OnBeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(timeOfMarginBeforeNote);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(timeOfMarginAfterNote);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - marginTime);
        #endregion
    }

    #region Reaction Times Methods
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

    #region Regular events
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
    #endregion

}
