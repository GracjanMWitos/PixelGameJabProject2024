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
    [SerializeField] private float mistakeMargin = 0.1f; // fraction of time between beat and halfbeat to give mistake margin before and after note
    [SerializeField] private float reactionCheckDelay = 0;
    [Range(0f, 2f)] [SerializeField] private float audioDelay = 0;
    public float notesDelay;
    Coroutine tactCoroutine;
    [Space]

    public float timeBetweenHalfbeats;
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
        moveIndicator = AttachMoveIndicatorToPlayer();

        #region Calculation for loops

        timeBetweenHalfbeats = (60/ beatPerMinute)/2 ;
        mistakeMargin = timeBetweenHalfbeats / mistakeMargin;
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

    private IEnumerator Loop()
    {

        #region 1 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        audioManager.PlayMetronomBeat();

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();                          

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 2 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 3 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 4 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 5 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 6 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 7 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 8 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 9 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 10 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 11 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 12 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 13 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 14 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 15 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);
        #endregion

        #region 16 beat and halfbeat
        BeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnBeat();
        EnemyTurn();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        BeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin);

        HalfbeatReactionTimeStart();
        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);

        OnHalfbeat();

        yield return new WaitForSecondsRealtime(mistakeMargin / notesDelay);
        HalfbeatReactionTimeEnd();

        yield return new WaitForSecondsRealtime(timeBetweenHalfbeats - mistakeMargin - mistakeMargin/notesDelay);
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
