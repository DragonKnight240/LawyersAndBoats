using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    public float speed = 2.0f;
    public float splashRadius = 2.0f;

    public int electricTargets = 1;
    private int targetsZapped;

    public bool explosive = false;
    public bool electrifying = false;

    public int damage;


    public void Track(Transform targetPos)
    {
        target = targetPos;
    }


    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject); // maybe add effect here
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
        //maybe switch statement after more effects 
        if (explosive)
        {
            Debug.Log("Exploded");
            Explode();
        }
        else if (electrifying)
        {
            Debug.Log("Zapped");
            Damage();
            ChainLightning();
        }
        else
        {
            Debug.Log("Normal Hit");
            Damage();
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, splashRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    void ChainLightning()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, splashRadius);
        for (int i = 0; i < colliders.Length; i++)                            
        {                      
            if (colliders[i].gameObject == target.gameObject)
            {
                continue;
            }
            if (colliders[i].tag == "Enemy" && targetsZapped < electricTargets)
            {
                colliders[i].GetComponent<Enemy>().TakeDamage(damage);
                targetsZapped++;
            }
        }
    }

    void Damage()
    {
        target.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, splashRadius);
    }
}