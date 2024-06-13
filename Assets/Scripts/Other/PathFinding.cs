using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding
{
    /*public List<GridTile> FindPath(GridTile start, GridTile end)
    {
        List<GridTile> tilesListToCheck = new List<GridTile>();
        List<GridTile> checkedTilesList = new List<GridTile>();

        tilesListToCheck.Add(start);

        while (tilesListToCheck.Count > 0)
        {
            GraphNode currentGridTile = tilesListToCheck.OrderBy(x => x.F).First();

            tilesListToCheck.Remove(currentGridTile);
            checkedTilesList.Add(currentGridTile);

            if (currentGridTile == end)
            {

                return GetFinishList(start, end);
            }

            var neightbourTiles = currentGridTile.GetConnections();

            foreach (var neighbour in neightbourTiles)
            {
                if (neighbour || checkedTilesList.Contains(neighbour.node))
                {
                    continue;
                }

                neighbour.G = GetManhettenDistance(start, neighbour);
                neighbour.H = GetManhettenDistance(end, neighbour);

                neighbour.previousTile = currentGridTile;

                if (!tilesListToCheck.Contains(neighbour))
                {
                    tilesListToCheck.Add(neighbour);
                }
            }
        }
        return new List<GridTile>();
    }

    private List<GridTile> GetFinishList(GridTile start, GridTile end)
    {
        List<GridTile> finishedList = new List<GridTile>();

        GridTile currentTile = end;
        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previousTile;
        }
        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhettenDistance(GridTile start, GridTile neighnour)
    {
        return Mathf.Abs(start.gridTileKey.x - neighnour.gridTileKey.x) + Mathf.Abs(start.gridTileKey.y - neighnour.gridTileKey.y);
    }*/
}
