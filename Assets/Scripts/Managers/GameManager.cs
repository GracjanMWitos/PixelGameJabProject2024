using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviorSingleton<GameManager>
{
    #region Assignments
    [Header("Assignments")]
    //Assigning via inspector
    [SerializeField] private GameObject moveIndicator;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    [SerializeField] private LevelFinishingSequence levelFinishingSequence;
    [SerializeField] private BeatManager beatManager;

    //Assigning via code
    [NonSerialized] public GameObject player;
    [NonSerialized] public GridTile currentPlayerTile;
    private PlayerController playerController;
    [Space]
    public bool noEnemies = true;
    #endregion

    [NonSerialized] public bool isBeat;
    [NonSerialized] public bool isHalfbeat;
    private bool canInvokeHalfbeatAction = true;

    public float audioDelay;

    private int invokeCountToHalfbeat;
    private int invokeCountToHalfbeatAction;
    private int invokeCountToBeat;
    private int invokeCountToEnemyTurn;

    private bool firstCountDown = true;
    private bool secondCountDown = true;
    private bool enemyTurnCooldown = true;

    [SerializeField] private int delayIndincator;
    [SerializeField] private int delayNotes;

    protected override void Awake()
    {
        base.Awake();
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

        StartCoroutine(DelayAudio());
    }
    private GameObject AttachMoveIndicatorToPlayer()
    {
        return Instantiate(moveIndicator, player.transform.position, Quaternion.identity, player.transform);
    }
    #endregion

    public void FinishLevel()
    {
        levelFinishingSequence.enabled = true;
        noEnemies = true;
    }
    public void GameOver()
    {
        GUIManager.Instance.DeathScreanActivation();
    }
    public float GetTimeBetweenHeafbeats()
    {
        return 60f / beatManager.bpm / 2;
    }

    private IEnumerator DelayAudio()
    {
        yield return new WaitForSecondsRealtime(audioDelay);
        beatManager.audioSource.Play();
    }
    private void EnemyTurn()
    {
        if (EnemiesManager.Instance.enemyControllers.Count > 0)
        {
            EnemiesManager.Instance.ExecuteEnemiesActions();
        }
    }
    #region Invoking after increasing index
    public void ExecuteCounterIncreasing()
    {
        IncreaseCounterToBeatCheck();
        IncreaseCounterToHalfbeatCheck();
        IncreaseCounterToUnconditionalActions();
        IncreaseCounterToEnemyTurn();
    }
    private void IncreaseCounterToUnconditionalActions()
    {
        invokeCountToHalfbeatAction++;

        if (invokeCountToHalfbeatAction == 4 + delayNotes && canInvokeHalfbeatAction)
        {
            GUIManager.Instance.MoveNotesToNextTarget();
            canInvokeHalfbeatAction = false;
        }
        else if (invokeCountToHalfbeatAction == 8 + delayNotes && !canInvokeHalfbeatAction)
        {
            invokeCountToHalfbeatAction = delayNotes;
            canInvokeHalfbeatAction = true;
        }
    }
    private void IncreaseCounterToHalfbeatCheck()
    {
        invokeCountToHalfbeat++;

        if (invokeCountToHalfbeat == 6 && isHalfbeat && !firstCountDown)
        {
            invokeCountToHalfbeat = 0;
            isHalfbeat = false;
            playerController.canShot = true;
        }
        else if (invokeCountToHalfbeat == 2 && !isHalfbeat && !firstCountDown)
        {
            invokeCountToHalfbeat = 0;
            isHalfbeat = true;
        }
        //First counting
        else if (invokeCountToHalfbeat == 5 + delayIndincator && !isHalfbeat && firstCountDown)
        {
            firstCountDown = false;
            invokeCountToHalfbeat = 0;
            isHalfbeat = true;
        }
    }

    private void IncreaseCounterToBeatCheck()
    {
        invokeCountToBeat++;

        if (invokeCountToBeat == 6 && isBeat && !secondCountDown)
        {
            invokeCountToBeat = 0;
            isBeat = false;
            playerController.canMove = true;
            moveIndicator.SetActive(false);
        }
        else if (invokeCountToBeat == 10 && !isBeat && !secondCountDown)
        {
            invokeCountToBeat = 0;
            isBeat = true;
            moveIndicator.SetActive(true);
        }
        //First counting
        else if (invokeCountToBeat == 13 + delayIndincator && !isBeat && secondCountDown)
        {
            secondCountDown = false;
            invokeCountToBeat = 0;
            moveIndicator.SetActive(true);
            isBeat = true;
        }
    }
    private void IncreaseCounterToEnemyTurn()
    {
        invokeCountToEnemyTurn++;

        if (invokeCountToEnemyTurn == 4 && enemyTurnCooldown)
        {
            invokeCountToEnemyTurn = 0;
            enemyTurnCooldown = false;
        }
        else if (invokeCountToEnemyTurn == 12 + delayIndincator && !enemyTurnCooldown)
        {
            invokeCountToEnemyTurn = 0;
            enemyTurnCooldown = true;
            EnemyTurn();
        }
    }
    #endregion
}
