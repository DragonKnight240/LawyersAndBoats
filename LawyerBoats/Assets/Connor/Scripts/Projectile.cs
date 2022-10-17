using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    public float speed = 2.0f;
    public float explosionRadius = 2.0f;
    public bool explosive = false;

    public void Track(Transform targetPos)
    {
        target = targetPos;
    }


    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceUpdate = speed * Time.deltaTime;

        if (direction.magnitude <= distanceUpdate)
        {
            //enemy hit
            Hit();
            return;
        }

        transform.Translate(direction.normalized * distanceUpdate, Space.World);

    }

    void Hit()
    {
        if (explosive)
        {
            Debug.Log("Exploded");
            Explode();
        }
        else
        {
            Debug.Log("Fail");
            Damage();
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Destroy(collider.gameObject);
            }
        }
    }

    void Damage()
    {
        Destroy(target.gameObject);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}