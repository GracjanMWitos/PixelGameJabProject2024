using System;
using System.Collections;
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
    [SerializeField] private LevelFinishingSequence levelFinishingSequence;
    [SerializeField] private BeatManager beatManager;

    //Assigning via code
    [HideInInspector] public GameObject player;
    [HideInInspector] public GridTile currentPlayerTile;
    private PlayerController playerController;
    [Space]
    public bool noEnemies = true;
    #endregion

    [HideInInspector] public bool isBeat;
    [HideInInspector] public bool isHalfbeat;

    public float audioDelay;

    [SerializeField] private int invokeCountToHalfbeat;
    [SerializeField] private int invokeCountToBeat;
    [SerializeField] private int invokeCountToEnemyTurn;
    public bool firstCountDown;
    public bool secondCountDown;
    public bool enemyTurnCooldown;

    [SerializeField] private int delayPoints;

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



        StartCoroutine(DelayAudio());

    }
    public float GetTimeBetweenHeafbeats()
    {
        return 60f / beatManager.bpm / 2;  
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

    private IEnumerator DelayAudio()
    {
        yield return new WaitForSecondsRealtime(audioDelay);
        audioManager.audioSource.Play();
    }
    

    public void OnBeat()
    {
        
        
    }
    public void OnHalfbeat()
    {
        GUIManager.Instance.MoveNotesToNextTarget();

    }
    public void EnemyTurn()
    {
        if (EnemiesManager.Instance.enemyControllers.Count > 0)
        {
            EnemiesManager.Instance.ExecuteEnemiesActions();

        }
    }
    #region Invoking events after increasing index
    public void InvokeHalfbeatCheckAfterCountDown()
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
        #region First counting
        else if (invokeCountToHalfbeat == 5 + delayPoints && !isHalfbeat && firstCountDown)
        {
            firstCountDown = false;
            invokeCountToHalfbeat = 0;
            isHalfbeat = true;
        }
        #endregion
    }

    public void InvokeBeatCheckAfterCountDown()
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
        #region First counting
        else if (invokeCountToBeat == 13 + delayPoints && !isBeat && secondCountDown)
        {
            secondCountDown = false;
            invokeCountToBeat = 0;
            moveIndicator.SetActive(true);
            isBeat = true;
        }
        #endregion
    }
    public void InvokeEnemyTurnAfterCountDown()
    {
        invokeCountToEnemyTurn++;

        if (invokeCountToEnemyTurn == 4 && enemyTurnCooldown)
        {
            invokeCountToEnemyTurn = 0;
            enemyTurnCooldown = false;
        }
        else if (invokeCountToEnemyTurn == 12 + delayPoints && !enemyTurnCooldown)
        {
            invokeCountToEnemyTurn = 0;
            enemyTurnCooldown = true;
            EnemyTurn();
        }
    }
    #endregion
}
