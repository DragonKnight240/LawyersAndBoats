using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public Transform target;

    public string enemyTag = "Enemy";

    public bool usesProjectile = true;

    public float radius = 10f;
    public float fireRate = 1f;
    public float rotateSpeed = 2f;

    public GameObject projectileObject;
    public Transform projectileOrigin;
    public Transform towerRotate;

    public int damage = 10;

    [SerializeField] int cost = 5;

    private float fireTimer = 0f;

    void Start()
    {
        InvokeRepeating("FindNearbyEnemies", 0f, 0.5f);
    }

    public void FindNearbyEnemies()
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

    public int GetCost()
    {
        return cost;
    }

    void Update()
    {
        if (target == null)
            return;

        TowerRotate();

        if (usesProjectile)
        {
            if (fireTimer <= 0.0f)
            {
                Fire();
                fireTimer = 1f / fireRate;
            }

                fireTimer -= Time.deltaTime;
        }
    }

    void Fire()
    {
        Debug.Log("Fire");
        GameObject newProjectile = (GameObject)Instantiate(projectileObject, projectileOrigin.position, projectileOrigin.rotation);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.damage = damage;

        if (projectile != null)
        {
            projectile.Track(target);
        }
    }

    void TowerRotate()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion look = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(towerRotate.rotation, look, Time.deltaTime * rotateSpeed).eulerAngles;
        towerRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}