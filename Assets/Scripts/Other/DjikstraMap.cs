using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DjikstraMap
{
    public List<GridTile> Mapping(GraphNode startTile)
    {
        ResetDistanceFromTilesToPlayer();

        List<GridTile> map = new();
        List<GraphNode> nodesListToCheck = new();

        nodesListToCheck.Add(startTile);

        while (nodesListToCheck.Count > 0)
        {
            var currentNode = (GridTile)nodesListToCheck.First();

            nodesListToCheck.Remove(currentNode);
            map.Add(currentNode);

            foreach (GraphNode.ConnectionInfo connection in currentNode.GetConnections())
            {
                var distanceMadeThisPath = currentNode.DistanceFromPlayer + connection.Distance; // current node distance from player + distance from current tile to one that is checking now

                if (!map.Contains(connection.tile) && (connection.tile.DistanceFromPlayer > distanceMadeThisPath || connection.tile.DistanceFromPlayer == 0))
                {
                    connection.tile.DistanceFromPlayer = distanceMadeThisPath;

                    if (!nodesListToCheck.Contains(connection.tile))
                        nodesListToCheck.Add(connection.tile);
                }
            }
        }
        return map;
    }

    private void ResetDistanceFromTilesToPlayer()
    {
        foreach (GridTile tile in GridManager.Instance.gridTilesMap.Values)
            tile.DistanceFromPlayer = 0;
    }
}
