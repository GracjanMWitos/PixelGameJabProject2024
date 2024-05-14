using System.Collections;
using System.Collections.Generic;
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
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    

    #region Visuals
    public void ShowEnemyTile()
    {
        spriteRenderer.color = damagingTileColor;
        tileHolder = TileHolder.Enemy;
    }
    public void ShowPlayerTile()
    {
        spriteRenderer.color = playerTileColor;
        tileHolder = TileHolder.Player;
    }
    public void HideTile()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        tileHolder = TileHolder.Nobody;
    }
    #endregion
}
