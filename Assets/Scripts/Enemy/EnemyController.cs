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
    private float timeBetweenMoves;

    //Variables without assigning
    [SerializeField] private int damage;

    private PathFinding pathFinding;
    private GridTile attackTargetGridTile;
    private List<GridTile> path = new List<GridTile>();
    private bool attackPerperation;

    [HideInInspector] public int numberOfMovesPerBeat = 1;
    private void Start()
    {
        pathFinding = new PathFinding();
        currentEnemyTile = GetEnemyTile(transform.position);
    }

    private GridTile GetEnemyTile(Vector3 position)
    {
        GridTile startingTile = null;
        var enemyTileKey = new Vector2Int((int)position.x, (int)position.y);

        foreach (Vector3Int tileInDictionary in GridManager.Instance.tilesLocationList)
        {
            if (GridManager.Instance.gridTilesMap.ContainsKey(enemyTileKey))
            {
                startingTile = GridManager.Instance.gridTilesMap[enemyTileKey];
            }
        }
        return startingTile;
    }
    private void SelectNewPath()
    {
            path.Clear();
            playerTile = GameManager.Instance.currentPlayerTile;
            path = pathFinding.FindPath(currentEnemyTile, playerTile); //Geting list of tiles that creating path to player
    }
    public void ExecuteEnemyAction()
    {
        if (currentEnemyTile != null && playerTile != null)
        {
            SelectNewPath();
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
    private void DealDamageToPlayer()
    {
        if (path[0] == playerTile)
        {
            if(playerHealthController == null)
                playerHealthController = GameManager.Instance.player.GetComponent<HealthController>();

            playerHealthController.TakeDamage(damage);
        }
    }

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
}
