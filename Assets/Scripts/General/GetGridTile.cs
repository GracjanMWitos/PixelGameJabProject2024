using UnityEngine;

public class GetGridTile
{
    public GridTile GetTile(Vector3 position)
    {
        GridTile startingTile = null;
        var tileKey = new Vector2Int((int)position.x, (int)position.y);

        foreach (Vector3Int tileInDictionary in GridManager.Instance.tilesLocationList)
        {
            if (GridManager.Instance.gridTilesMap.ContainsKey(tileKey))
            {
                startingTile = GridManager.Instance.gridTilesMap[tileKey];
            }
        }
        return startingTile;
    }
}
