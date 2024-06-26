using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviorSingleton<EnemiesManager>
{

    public List<EnemyController> enemyControllers = new List<EnemyController>();
    [SerializeField] private Transform currentEnemiesGroupTransform;

    protected override void Awake()
    {
        base.Awake();
    }
    public void ActivateEnemies()
    {
        RefreshEnemiesList();

        int totalEnemiesHealthCount = 0;
        foreach (EnemyController enemyController in enemyControllers)
        {
            enemyController.isFightStarted = true;
            enemyController.spriteRenderer.enabled = true;
            totalEnemiesHealthCount += enemyController.GetComponent<HealthController>().maxHealthPoints;
        }
        GUIManager.Instance.GetEnemiesTotalHealthPointsCount(totalEnemiesHealthCount);
    }
    public void RefreshEnemiesList()
    {
        enemyControllers.Clear();
        EnemyController[] tempEnemiesArray = currentEnemiesGroupTransform.GetComponentsInChildren<EnemyController>();
        if (tempEnemiesArray.Length > 0)
        {
            foreach (EnemyController enemyController in tempEnemiesArray)
            {
                enemyController.playerTile = GameManager.Instance.currentPlayerTile;
                enemyControllers.Add(enemyController);
            }
        }
        else
        {
            GameManager.Instance.FinishLevel();
        }

    }
    public void ExecuteEnemiesActions()
    {
        RefreshEnemiesList();
        foreach (EnemyController enemyController in enemyControllers)
        { 
            if (enemyController != null)
            {
                enemyController.ExecuteEnemyAction();
            }
        }
    }
}
