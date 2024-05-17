using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    private GetGridTile getGridTile = new GetGridTile();
    void Update()
    {
        if (getGridTile.GetTile(transform.position) == GameManager.Instance.currentPlayerTile)
        {
            EnemiesManager.Instance.ActivateEnemies();
        }
    }
}
