using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private static EnemiesManager _instance;
    public static EnemiesManager Instance { get { return _instance; } }

    public Transform[] enemiesArray;
    private EnemyController[] enemyControllers;
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
        RefreshEnemiesArray();
    }
    public Transform[] RefreshEnemiesArray()
    {
        Transform[] tempEnemiesArray = new Transform[currentEnemiesGroupTransform.childCount];
        for (int i = 0; i < currentEnemiesGroupTransform.childCount; i++)
        {
            tempEnemiesArray[i] = currentEnemiesGroupTransform.GetChild(i);
        }
        enemiesArray = tempEnemiesArray;
        return enemiesArray;
    }
    public void MoveAllEnemies()
    {
        for (int i = 0; i < currentEnemiesGroupTransform.childCount; i++)
        {
            
            enemiesArray[i].GetComponent<EnemyController>().EnemyMove();
            Debug.Log("Move enemy with index " + i);
        }
    }
}
