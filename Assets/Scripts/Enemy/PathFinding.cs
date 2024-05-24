using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding
{
    public List<GridTile> FindPath(GridTile start, GridTile end)
    {
        List<GridTile> openList = new List<GridTile>();
        List<GridTile> closedList = new List<GridTile>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            GridTile currentGridTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentGridTile);
            closedList.Add(currentGridTile);

            if (currentGridTile == end)
            {

                return GetFinishList(start, end);
            }

            var neightbourTiles = GetNeighbourTiles(currentGridTile);

            foreach (var neighbour in neightbourTiles)
            {
                if (neighbour.isBlocked || closedList.Contains(neighbour))
                {
                    continue;
                }

                neighbour.G = GetManhettenDistance(start, neighbour);
                neighbour.H = GetManhettenDistance(end, neighbour);

                neighbour.previousTile = currentGridTile;

                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
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
        return Mathf.Abs(start.gridTileLocation.x - neighnour.gridTileLocation.x) + Mathf.Abs(start.gridTileLocation.y - neighnour.gridTileLocation.y);
    }

    private List<GridTile> GetNeighbourTiles(GridTile currentGridTile)
    {
        var gridTilesMap = GridManager.Instance.gridTilesMap;

        List<GridTile> neighbourTiles = new List<GridTile>();

        //top neighbour
        Vector2Int locationToCheck = new Vector2Int(
            currentGridTile.gridTileLocation.x,
            currentGridTile.gridTileLocation.y + 1
            );

        if (gridTilesMap.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(gridTilesMap[locationToCheck]);
        }
        //bottom neighbour
        locationToCheck = new Vector2Int(
            currentGridTile.gridTileLocation.x,
            currentGridTile.gridTileLocation.y - 1
            );

        if (gridTilesMap.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(gridTilesMap[locationToCheck]);
        }
        //left neighbour
        locationToCheck = new Vector2Int(
            currentGridTile.gridTileLocation.x - 1,
            currentGridTile.gridTileLocation.y
            );

        if (gridTilesMap.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(gridTilesMap[locationToCheck]);
        }
        //right neighbour
        locationToCheck = new Vector2Int(
            currentGridTile.gridTileLocation.x + 1,
            currentGridTile.gridTileLocation.y
            );

        if (gridTilesMap.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(gridTilesMap[locationToCheck]);
        }

        return neighbourTiles;
    }

}
