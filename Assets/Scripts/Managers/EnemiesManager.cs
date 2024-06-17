using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviorSingleton<EnemiesManager>
{

    public List<EnemyController> enemyControllers = new List<EnemyController>();
    [SerializeField] private Transform currentEnemiesGroupTransform;
    private bool isFightStarted = false;
    private GetGridTile getGridTile = new();

    protected override void Awake()
    {
        base.Awake();
    }
    public void ActivateEnemies()
    {
        RefreshEnemiesList();

        int totalEnemiesHealthCount = 0;
        isFightStarted = true;
        foreach (EnemyController enemyController in enemyControllers)
        {
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
            foreach (EnemyController enemyController in tempEnemiesArray)
            {
                enemyController.playerTile = GameManager.Instance.currentPlayerTile;
                // get enemy tile, save distance fromplayer in enemy controller, 
                var enemyTile = getGridTile.GetTile(enemyController.transform.position);
                enemyController.DistanceFromPlayer = enemyTile.DistanceFromPlayer;

                enemyControllers.Add(enemyController);
            }
        else
            GameManager.Instance.FinishLevel();

        enemyControllers = enemyControllers.OrderBy(x => x.DistanceFromPlayer).ToList();
    }
    public void ExecuteEnemiesActions()
    {
        if (isFightStarted)
        {
            RefreshEnemiesList();

            foreach (EnemyController enemyController in enemyControllers)
                if (enemyController != null)
                {
                    enemyController.ExecuteEnemyAction();
                }
        }
    }
}
