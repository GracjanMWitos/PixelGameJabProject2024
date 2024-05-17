using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private int projectileDamage = 1;
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
        Move();
        if (Vector2.Distance(transform.position, target.position) < destoyDistanceBeforeTarget)
        {
            DestroyProjectile();
        }
    }
    private void Move()
    {
        Vector2 direction = target.position - transform.position;
        projectileRB.AddForce(direction * projectileSpeed/4);
        projectileRB.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed * gameSpeed * Time.deltaTime;
    }
    private void DestroyProjectile()
    {
        target.GetComponent<HealthController>().TakeDamage(projectileDamage);
        Destroy(gameObject);
    }
    private Transform ClosestTarget()
    {
        List<EnemyController> arrayOfEnemiesToCheck = EnemiesManager.Instance.enemyControllers;
        float[] distancesFromProjectlieToEnemies = new float[arrayOfEnemiesToCheck.Count];

        for (int i = 0; i < distancesFromProjectlieToEnemies.Length; i++)
        {
            distancesFromProjectlieToEnemies[i] = Vector3.Distance(transform.position, arrayOfEnemiesToCheck[i].transform.position);
        }

        for (int j = 0; j < distancesFromProjectlieToEnemies.Length; j++)
        {
            if (distancesFromProjectlieToEnemies[j] == distancesFromProjectlieToEnemies.Min())
            {
                return arrayOfEnemiesToCheck[j].transform;
            }
        }
        return null;
    }
    public void SelectTarget()
    {
        target = ClosestTarget();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
