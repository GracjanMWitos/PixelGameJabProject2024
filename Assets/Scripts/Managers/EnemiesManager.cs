using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private static EnemiesManager _instance;
    public static EnemiesManager Instance { get { return _instance; } }

    public List<EnemyController> enemyControllers = new List<EnemyController>();
    [SerializeField] private Transform currentEnemiesGroupTransform;

    void Awake()
    {
        #region Instance check
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
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
