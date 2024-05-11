using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    public static GridManager Instance { get { return _instance; } }

    [SerializeField] private GridTile gridTilePrefab = null;
    [SerializeField] private Transform gridContainer = null;

    public List<Vector3> tilesLocationList = new List<Vector3>();


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        Tilemap tilemap = gameObject.GetComponentInChildren<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tileLocation = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tileLocation))
                {
                    var gridTile = Instantiate(gridTilePrefab, tileLocation, Quaternion.identity, gridContainer);
                    tilesLocationList.Add(tileLocation);
                }
            }
        }
    }
    public Vector3 CheckNewTile(Vector3 currentTile, Vector3 nextTile)
    {
        foreach (Vector3 tileLocation in tilesLocationList)
        {
            if (nextTile == tileLocation)
            {
                return nextTile;
            }
        }
        return currentTile;
    }
}


