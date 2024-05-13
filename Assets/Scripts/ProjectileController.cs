using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D projectileRB;
    private float gameSpeed; // beats per minute
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float destoyDistanceBeforeTarget = 0.25f;
    private void Awake()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        SelectTarget();

    }
    private void Start()
    {
        gameSpeed = GameManager.Instance.beatPerMinute;
    }
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        projectileRB.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed * gameSpeed * Time.deltaTime;
        if (Vector2.Distance(transform.position, target.position) < destoyDistanceBeforeTarget)
        {
            Destroy(gameObject);
        }
    }

    private Transform ClosestTarget()
    {
        Transform[] arrayOfEnemiesToCheck = EnemiesManager.Instance.RefreshEnemiesArray();
        float[] distancesFromProjectlieToEnemies = new float[arrayOfEnemiesToCheck.Length];

        for (int i = 0; i < distancesFromProjectlieToEnemies.Length; i++)
        {
            distancesFromProjectlieToEnemies[i] = Vector3.Distance(transform.position, arrayOfEnemiesToCheck[i].position);
        }

        for (int j = 0; j < distancesFromProjectlieToEnemies.Length; j++)
        {
            if (distancesFromProjectlieToEnemies[j] == distancesFromProjectlieToEnemies.Min())
            {
                return arrayOfEnemiesToCheck[j];
            }
        }
        return null;
    }
    public void SelectTarget()
    {
        target = ClosestTarget();
    }
}
