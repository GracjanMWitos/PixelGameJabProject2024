using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    #region Assignments
    InputActions inputActions;
    [SerializeField] private Transform ProjectileSpwanPoint;
    [SerializeField] private GameObject[] projectiles;
    #endregion

    Vector3 currentTile;

    private void Awake()
    {
        inputActions = new InputActions();

    }
    private void Start()
    {
        GetPlayerTile();
    }
    private void Move(int xAxisValue, int yAxisValue)
    {
        if (GameManager.Instance.isBeat)
        {
            currentTile = transform.position;
            Vector3 nextTile = new Vector3(currentTile.x + xAxisValue, currentTile.y + yAxisValue, currentTile.z);
            transform.position = CheckNextTile(currentTile, nextTile);
            AudioManager.Instance.PlayKick();
            GetPlayerTile();
        }
    }
    private GridTile GetPlayerTile()
    {
        GridTile currentTile = null;
        var playerTileKey = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        foreach (Vector3Int tileInDictionary in GridManager.Instance.tilesLocationList)
        {
            if (GridManager.Instance.gridTilesMap.ContainsKey(playerTileKey))
            {
                currentTile = GridManager.Instance.gridTilesMap[playerTileKey];
                GameManager.Instance.currentPlayerTile = currentTile; //Update player tile in Game Manager
            }
        }
        return currentTile;
    }
    private Vector3 CheckNextTile(Vector3 currentTile, Vector3 nextTile)
    {
        foreach (Vector3 tileLocation in GridManager.Instance.tilesLocationList)
        {
            if (nextTile == tileLocation)
            {
                return nextTile;
            }
        }
        return currentTile;
    }

    private void OnHitNote(int projectileIndex)
    {
        if (GameManager.Instance.isHalfbeat || GameManager.Instance.isBeat)
        {
            Instantiate(projectiles[projectileIndex], ProjectileSpwanPoint.position, Quaternion.identity);
            AudioManager.Instance.PlayHiHat();
        }
    }
    private void Update()
    {
        #region Movement
        inputActions.Player.MoveUp.performed += ctx => Move(0, 1);
        inputActions.Player.MoveDown.performed += ctx => Move(0, -1);
        inputActions.Player.MoveLeft.performed += ctx => Move(-1, 0);
        inputActions.Player.MoveRight.performed += ctx => Move(1, 0);
        #endregion

        inputActions.Player.HitNote.performed += ctx => OnHitNote(0);
    }
    #region Enable Disable
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    #endregion
}

