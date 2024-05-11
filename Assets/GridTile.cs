using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileHolder {Nobody, Player, Enemy, FullObstacle, MovementObstacle}
public class GridTile : MonoBehaviour
{
    [HideInInspector] public TileHolder tileHolder;
    [SerializeField] private Color playerTileColor;
    [SerializeField] private Color enemyTileColor;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    #region Visuals
    public void ShowEnemyTile()
    {
        spriteRenderer.color = enemyTileColor;
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
