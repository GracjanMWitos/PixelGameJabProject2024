using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DjikstraMap
{
    public List<GridTile> Mapping(GraphNode startTile)
    {
        ResetTiles();

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
                var distanceMadeThisPath = currentNode.DistancFromPlayer + connection.Distance; // current node distance from player + distance from current tile to one that is checking now

                if (!map.Contains(connection.tile) && (connection.tile.DistancFromPlayer > distanceMadeThisPath || connection.tile.DistancFromPlayer == 0))
                {
                    connection.tile.DistancFromPlayer = distanceMadeThisPath;

                    if (!nodesListToCheck.Contains(connection.tile))
                        nodesListToCheck.Add(connection.tile);
                }
            }
        }
        return map;
    }

    private void ResetTiles()
    {
        foreach (GridTile tile in GridManager.Instance.gridTilesMap.Values)
            tile.DistancFromPlayer = 0;
    }
}
