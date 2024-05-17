using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Assigning via inspector

    //Assigning via code
    public GridTile playerTile;
    [SerializeField] private GridTile currentEnemyTile;
    private HealthController playerHealthController;
    public SpriteRenderer spriteRenderer;
    private float timeBetweenMoves;

    //Variables without assigning
    [SerializeField] private int damage;

    private PathFinding pathFinding;
    private GridTile attackTargetGridTile;
    private List<GridTile> path = new List<GridTile>();
    private bool attackPerperation;

    [HideInInspector] public int numberOfMovesPerBeat = 1;

    [HideInInspector] public bool isFightStarted;

    private GetGridTile getGridTile = new GetGridTile();
    private void Start()
    {
        pathFinding = new PathFinding();
        currentEnemyTile = getGridTile.GetTile(transform.position);
    }

    public void ExecuteEnemyAction()
    {
        if (currentEnemyTile != null && playerTile != null && isFightStarted)
        {
            RefreshPath();
            if (path[0] != playerTile && !attackPerperation)
            {
                EnemyMove();
            }
            else if (attackPerperation)
            {
                DealDamageToPlayer();
                attackPerperation = false;
                return;
            }
            else if (path[0] == playerTile)
            {
                attackPerperation = true;
            }
        }
    }
    private void RefreshPath()
    {
        path.Clear();
        playerTile = GameManager.Instance.currentPlayerTile;
        path = pathFinding.FindPath(currentEnemyTile, playerTile); //Geting list of tiles that creating path to player
    }
    #region Movement
    private void EnemyMove()
    {
        timeBetweenMoves = GameManager.Instance.timeBetweenHalfbeats;
        transform.position = Vector3.Lerp(transform.position, SelectNextMove(), timeBetweenMoves / Time.deltaTime);

    }
    private Vector3 SelectNextMove()
    {
        currentEnemyTile = path[0];
        path.RemoveAt(0);
        if (path.Count == 1)
        {
            attackPerperation = true;
        }
        return currentEnemyTile.transform.position;
    }
    #endregion
    private void DealDamageToPlayer()
    {
        if (path[0] == playerTile)
        {
            if (playerHealthController == null)
                playerHealthController = GameManager.Instance.player.GetComponent<HealthController>();

            playerHealthController.TakeDamage(damage);
        }
    }
}
