using UnityEngine;

public enum TileHolder {Nobody, Player, Enemy, FullObstacle, MovementObstacle}
public class GridTile : MonoBehaviour
{
    public int G; // G Cost
    public int H; // H Cost
    public int F { get { return G + H; } } // F Cost

    public bool isBlocked;

    public Vector3Int gridTileLocation;

    public GridTile previousTile;

    [HideInInspector] public TileHolder tileHolder;
    [SerializeField] private Color playerTileColor;
    [SerializeField] private Color damagingTileColor;
}
