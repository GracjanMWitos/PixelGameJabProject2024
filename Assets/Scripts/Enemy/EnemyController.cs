using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Assigning via code
    public GridTile playerTile;
    public GridTile neighbourTile;
    [System.NonSerialized] public GridTile currentEnemyTile;
    [System.NonSerialized] public GridTile previousEnemyTile;
    public SpriteRenderer spriteRenderer;
    private float timeBetweenMoves;


    [SerializeField] private int damage;
    private bool attackPerperation;

    [System.NonSerialized] public int numberOfMovesPerBeat = 1;

    private PathFinding pathFinding;
    private List<GridTile> path = new List<GridTile>();
    private GetGridTile getGridTile = new GetGridTile();

    private void Start()
    {
        pathFinding = new PathFinding();
        currentEnemyTile = (GridTile)getGridTile.GetTile(transform.position);
        //previousEnemyTile = currentEnemyTile;
    }

    public void ExecuteEnemyAction()
    {
        if (currentEnemyTile != null && playerTile != null)
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
        playerTile = GameManager.Instance.currentPlayerTile; // setting target on player


        //path = pathFinding.FindPath(currentEnemyTile, playerTile); //Geting list of tiles creating path to player

        /*var neighbourTileList = GetClosestPosibleTileToPlayer();

        foreach (GridTile neighbour in neighbourTileList)
        { 
            if (neighbour.isBlocked)
                continue;

            neighbourTile = neighbour;
            break;
        }


        //if path to player is blocked look for list of tiles creating path to neighbour closest to player
        if (path.Count == 0)
            path = pathFinding.FindPath(currentEnemyTile, neighbourTile);*/
    }
    /*private List<GridTile> GetClosestPosibleTileToPlayer()
    {
        var targetNeighboursList = pathFinding.GetNeighbourTiles(playerTile); //listOfPlayerNeighbours
        var availableNeighbours = new List<GridTile>();

        foreach (GridTile targetNeighbour in targetNeighboursList) // going though all players neighbours
        {
            List<GridTile> neighboursOfTargetNeighbour = pathFinding.GetNeighbourTiles(targetNeighbour); // getting current neighbour neighbours

            foreach (GridTile neighbour in neighboursOfTargetNeighbour)
                if (!neighbour.isBlocked || !availableNeighbours.Contains(neighbour))
                {
                    neighbour.playerTile = playerTile;

                    if (neighbour != playerTile)
                        availableNeighbours.Add(neighbour);
                }
        }
        availableNeighbours.OrderBy(x => x.distanceFromPlayer);
        return availableNeighbours;
    }*/
    #region Movement
    private void EnemyMove()
    {
        //getting time between moves matching to time between beats
        timeBetweenMoves = GameManager.Instance.GetTimeBetweenHeafbeats() / 10;
        //SetTilesRelatedToThisEnemy();

        StartCoroutine(Extns.SmoothTweeng(timeBetweenMoves,
            (p) => transform.position = p,
            previousEnemyTile.transform.position,
            currentEnemyTile.transform.position
           )
            );
    }
    /*private void SetTilesRelatedToThisEnemy()
    {
        // setting current tile that is ocupated before move as previous one and on unblocked
        previousEnemyTile = currentEnemyTile;
        previousEnemyTile.isBlocked = false;

        // setting the next tile from the path as the current one and blocking it for other enemies 
        currentEnemyTile = path[0];
        currentEnemyTile.isBlocked = true;

        // as next tile is saved, it can be removed to check check that the distance from the player is sufficient to prepare for an attack
        path.RemoveAt(0);
        if (path.Count == 1)
            attackPerperation = true;

    }*/
    #endregion
    private void DealDamageToPlayer()
    {
        if (path[0] == playerTile)
            GameManager.Instance.player.GetComponent<HealthController>().TakeDamage(damage);
    }
}
