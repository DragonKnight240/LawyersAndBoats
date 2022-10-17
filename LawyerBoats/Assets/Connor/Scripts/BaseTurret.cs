using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public Transform target;

    public float radius = 10f;
    public float fireRate = 1f;
    private float fireTimer = 0f;

    public string enemyTag = "Enemy";

    public GameObject projectileObject;
    public Transform projectileOrigin;


    void Start()
    {
        InvokeRepeating("FindNearbyEnemies", 0f, 1.0f);
    }

    void FindNearbyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float smallestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && smallestDistance <= radius)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Update()
    {
        if (target == null)
            return;

        if (fireTimer <= 0.0f)
        {
            Fire();
            fireTimer = 1f / fireRate;
        }

        fireTimer -= Time.deltaTime;

    }

    void Fire()
    {
        Debug.Log("Fire");
        GameObject newProjectile = (GameObject)Instantiate(projectileObject, projectileOrigin.position, projectileOrigin.rotation);
        Projectile projectile = newProjectile.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Track(target);
        }
    }
}