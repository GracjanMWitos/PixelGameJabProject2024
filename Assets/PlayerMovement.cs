using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputActions inputActions;
    Vector3 currentTile;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void Move(int xAxisValue, int yAxisValue)
    {
        Vector3 nextTile = new Vector3(currentTile.x + xAxisValue, currentTile.y + yAxisValue, currentTile.z);
        transform.position = GridManager.Instance.CheckNewTile(currentTile, nextTile);
        currentTile = transform.position;
    }
    private void Update()
    {
        inputActions.Player.MoveUp.performed += ctx => Move(0, 1);
        inputActions.Player.MoveDown.performed += ctx => Move(0, -1);
        inputActions.Player.MoveLeft.performed += ctx => Move(-1, 0);
        inputActions.Player.MoveRight.performed += ctx => Move(1, 0);
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}

