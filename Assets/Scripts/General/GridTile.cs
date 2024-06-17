using System;
using UnityEngine;
using System.Collections.Generic;
public enum TileHolder {Nobody, Player, Enemy, Damagable, FullObstacle, MovementObstacle}
public class GridTile : MonoBehaviour, GraphNode
{
    public int G; // G Cost
    public int H; // H Cost
    public int F { get { return G + H; } } // F Cost

    public Vector2Int gridTileKey;

    public GraphNode previousTile;

    [NonSerialized] public TileHolder tileHolder;

    [SerializeField] private Color playerTileColor;
    [SerializeField] private Color damagingTileColor;



    public float DistanceFromPlayer;

    public static Vector2Int[] directions = new Vector2Int[] {
            new(1, 0),
            new(-1, 0),
            new(0, 1),
            new(0, -1),
        };
    public IEnumerable<GraphNode.ConnectionInfo> GetConnections()
    {
        var gridTilesMapKey = GridManager.Instance.gridTilesMap; // tiles dictionary

        List<GraphNode.ConnectionInfo> nodesConnections = new(); // list of this tile neighbours and distances to them
        foreach (var direction in directions)
        {
            Vector2Int neighbourKey = gridTileKey + direction;
            
            if (gridTilesMapKey.ContainsKey(neighbourKey))
            {
                GraphNode.ConnectionInfo connectionInfo = new(); 
                connectionInfo.tile = gridTilesMapKey[neighbourKey];
                connectionInfo.Distance = Mathf.RoundToInt(Vector2.Distance(transform.position, neighbourKey)); // distance from this tile to neighbour key which is tile position 

                nodesConnections.Add(connectionInfo);
            }
        }
        return nodesConnections; 
    }
}