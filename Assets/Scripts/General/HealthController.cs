using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthPointHolder { Player, Enemy}
public class HealthController : MonoBehaviour
{
    public int maxHealthPoints = 10;
    private int currentHealthPoints;
    [SerializeField] private HealthPointHolder hPHolder;

    [Header("Debug Tools")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private int debugDamage = 0;
    [SerializeField] private int debugHeal = 0;
    private void Awake()
    {
        currentHealthPoints = maxHealthPoints;
    }

    public void TakeDamage(int damage)
    {
        currentHealthPoints -= damage;

        if (hPHolder == HealthPointHolder.Player)
        {
            if (currentHealthPoints < 0)
            {
                damage += currentHealthPoints;
                currentHealthPoints = 0;
            }
            GUIManager.Instance.LowerNumberOfHealthPointsInUI(currentHealthPoints, damage);
        }
        else if (hPHolder == HealthPointHolder.Enemy)
        {
            if (currentHealthPoints < 0)
            {
                damage = damage + currentHealthPoints;
            }
            GUIManager.Instance.LowerTotalEnemyHealthPointsCount(damage);
        }
        DeathCheck();
    }
    public void HealDamage(int heal)
    {
        currentHealthPoints += heal;

        if (hPHolder == HealthPointHolder.Player)
        {
            if (currentHealthPoints > maxHealthPoints)
            {
                heal -= currentHealthPoints - maxHealthPoints;
                currentHealthPoints = maxHealthPoints;
            }
            GUIManager.Instance.RaiseNumberOfHealthPointsInUI(currentHealthPoints, heal);
        }
        
    }
    private void DeathCheck()
    {
        if (currentHealthPoints <= 0)
        {
            if (hPHolder == HealthPointHolder.Player)
            {
                GameManager.Instance.GameOver();
            }
            if (hPHolder == HealthPointHolder.Enemy)
            {
                EnemiesManager.Instance.enemyControllers.Remove(GetComponent<EnemyController>());
                Destroy(gameObject);
            }
        }
    }
    private void Update()
    {
        if (debugMode)
        {
            if (debugDamage > 0)
            {
                TakeDamage(debugDamage);
                debugDamage = 0;
            }
            if (debugHeal > 0)
            {
                HealDamage(debugHeal);
                debugHeal = 0;
            }
        }
    }
}
