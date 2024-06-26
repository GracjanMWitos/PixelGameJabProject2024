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

    private Vector3 currentPosition;
    public bool canMove;
    public bool canShot;
    public GetGridTile getGridTile = new GetGridTile();
    private void Awake()
    {
        inputActions = new InputActions();

    }
    private void Start()
    {
        //GetPlayerTile();
    }
    private void Move(int xAxisValue, int yAxisValue)
    {
        if (GameManager.Instance.isBeat && canMove)
        {
            currentPosition = transform.position;
            Vector3 nextTile = new Vector3(currentPosition.x + xAxisValue, currentPosition.y + yAxisValue, currentPosition.z);
            transform.position = CheckNextTile(currentPosition, nextTile);
            GameManager.Instance.currentPlayerTile = getGridTile.GetTile(transform.position);

            canMove = false;
        }
        else
        {
            canMove = false;
        }
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

    private void Shot(int projectileIndex)
    {
        if ((GameManager.Instance.isHalfbeat || GameManager.Instance.isBeat) && canShot)
        {
            Instantiate(projectiles[projectileIndex], ProjectileSpwanPoint.position, Quaternion.identity);

            canShot = false;
        }
        else
        {
            canShot = false;
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

        inputActions.Player.HitNote.performed += ctx => Shot(0);
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

