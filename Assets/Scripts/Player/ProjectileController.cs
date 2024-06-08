using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //Assignments
    private Transform target;
    private Rigidbody2D projectileRB;
    [SerializeField] private Transform eventsActivator;

    [SerializeField] private int projectileDamage = 1;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float destoyDistanceBeforeTarget = 0.35f;
    private void Awake()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        eventsActivator = GameObject.FindGameObjectWithTag("EventsActivator").transform;

        SelectTarget();

    }
    void Update()
    {
        //At the level end change target on eventActivator
        if (GameManager.Instance.noEnemies)
        {
            target = eventsActivator;
        }
        // if there is no target search for one
        if (target == null)
        {
            target = ClosestTarget();
        }

        Move();
        // destroy projectile when it will get close enought to target
        if (Vector2.Distance(transform.position, target.position) < destoyDistanceBeforeTarget)
        {
            DestroyProjectile();
        }
    }
    private void Move()
    {
        Vector2 direction = target.position - transform.position;
        projectileRB.AddForce(direction * projectileSpeed/4);
        projectileRB.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed * Time.deltaTime;
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

    private void DestroyProjectile()
    {
        if (!GameManager.Instance.noEnemies)
        {
            target.GetComponent<HealthController>().TakeDamage(projectileDamage);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
