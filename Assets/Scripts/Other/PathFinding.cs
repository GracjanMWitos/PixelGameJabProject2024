using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding
{
    public GridTile GetNextTileOnPathToPlayer(GraphNode tile)
    {
        var connectionsToNeighbours = tile.GetConnections().OrderBy(x => x.tile.DistanceFromPlayer);
        var lowestDistance = connectionsToNeighbours.First().tile.DistanceFromPlayer;

        List<GridTile> neighboursWithShortestDistances = new();
        foreach (var connection in connectionsToNeighbours)
            if (connection.tile.DistanceFromPlayer == lowestDistance)
                neighboursWithShortestDistances.Add(connection.tile);

        return neighboursWithShortestDistances[Random.Range(0, neighboursWithShortestDistances.Count)];
    }
}
