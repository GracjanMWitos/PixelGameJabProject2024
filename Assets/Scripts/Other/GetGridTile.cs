using UnityEngine;

public class GetGridTile
{
    public GridTile GetTile(Vector3 position)
    {
        var tileKey = new Vector2Int((int)position.x, (int)position.y);

        if (GridManager.Instance.gridTilesMap.ContainsKey(tileKey))
            return GridManager.Instance.gridTilesMap[tileKey];

        return null;
    }
}
