using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private static EnemiesManager _instance;
    public static EnemiesManager Instance { get { return _instance; } }

    //public Transform[] enemiesArray;
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
    private void Start()
    {
        RefreshEnemiesList();
    }
    public void RefreshEnemiesList()
    {
        enemyControllers.Clear();
        EnemyController[] tempEnemiesArray = currentEnemiesGroupTransform.GetComponentsInChildren<EnemyController>();
        foreach (EnemyController enemyController in tempEnemiesArray)
        {
            enemyControllers.Add(enemyController);
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
