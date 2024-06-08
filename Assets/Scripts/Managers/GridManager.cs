using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridManager : MonoBehaviorSingleton<GridManager>
{
    //Assigning via inspector
    [SerializeField] private GridTile gridTilePrefab;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Tilemap groundTilemap;

    //Other
    [System.NonSerialized] public List<Vector3Int> tilesLocationList = new List<Vector3Int>();
    public Dictionary<Vector2Int, GridTile> gridTilesMap = new Dictionary<Vector2Int, GridTile>();

    protected override void Awake()
    {
        base.Awake();
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tileLocation = new Vector3Int(x, y, 0);
                Vector2Int tileKey = new Vector2Int(x, y);
                if (groundTilemap.HasTile(tileLocation) && !gridTilesMap.ContainsKey(tileKey))
                {
                    var gridTile = Instantiate(gridTilePrefab, tileLocation, Quaternion.identity, gridContainer);

                    gridTile.gridTileLocation = tileLocation;
                    gridTile.name = "tile " + tileKey;
                    tilesLocationList.Add(tileLocation);
                    gridTilesMap.Add(tileKey, gridTile);
                }
            }
        }
    }
}


