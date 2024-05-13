using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Assigning via inspector

    //Assigning via code
    [SerializeField] private GridTile playerTile;

    [SerializeField] private float timeBetweenMoves;
    [SerializeField] private float moveSpeed;
    private PathFinding pathFinding;
    public GridTile currentEnemyTile;
    [HideInInspector] public int numberOfMovesPerBeat = 1;
    public List<GridTile> path = new List<GridTile>();
    private void Start()
    {
        pathFinding = new PathFinding();

    }

    private GridTile GetEnemyTile()
    {
        GridTile startingTile = null;
        var enemyTileKey = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        foreach (Vector3Int tileInDictionary in GridManager.Instance.tilesLocationList)
        {
            if (GridManager.Instance.gridTilesMap.ContainsKey(enemyTileKey))
            {
                startingTile = GridManager.Instance.gridTilesMap[enemyTileKey];
            }
        }
        return startingTile;
    }
    void Update()
    {
        currentEnemyTile = GetEnemyTile();
    }
    public void EnemyMove()
    {
        timeBetweenMoves = GameManager.Instance.timeBetweenHalfbeats;

        playerTile = GameManager.Instance.currentPlayerTile;

        transform.position = Vector3.Lerp(transform.position, SelectNextMove(), timeBetweenMoves / Time.deltaTime);
        //path.RemoveAt(0);
    }
    private Vector3 SelectNextMove()
    {
        path.Clear();
        path = pathFinding.FindPath(currentEnemyTile, playerTile); //Geting list of tiles that creating path to player

        return path[0].transform.position;
    }
}
