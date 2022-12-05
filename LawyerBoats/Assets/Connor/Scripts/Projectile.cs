using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    private int targetsZapped;

    public float speed = 2.0f; // proj speed
    public float splashRadius = 2.0f; // splash damage radius for explosives/chain effects

    //on hit effects
    public bool explosive = false;
    public bool electrifying = false;
    public bool bleed = false;
    public bool bounces = false;

    public int electricTargets = 1; // additional targets to zap

    public int bouncesLeft = 3;

    public int damage; // tower collision damage

    public int bleedDamage; // Total bleed damage
    public int bleedTime; // Total bleed time
    public int bleedTick; // intervals between bleed damage;
    private int bleedRemaining;

    public void Track(Transform targetPos)
    {
        target = targetPos;
    }


    void Update()
    {

        if (target == null)
        {
            if (!bounces)
            {
                Destroy(this.gameObject); // maybe add effect here
            }
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
        else if (bleed)
        {
            Debug.Log("Bleeding");
            Damage();
            Bleeding();
        }
        else if (bounces)
        {
            Damage();
            Ricochet();
            if (bouncesLeft <= 0)
            {
                Destroy(this.gameObject);
            }
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

        Destroy(this.gameObject);
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

    void Bleeding()
    {
        bleedRemaining = bleedTime;

        while (bleedRemaining > 0)
        {

            float timer = bleedTick;

            if (timer >= bleedTick)
            {
                target.GetComponent<Enemy>().TakeDamage(bleedDamage/bleedTime);
                bleedRemaining -= 1;
            }

            timer += Time.deltaTime;
        }
    }

    void Ricochet()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, splashRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == target.gameObject)
            {
                continue;
            }
            else if (colliders[i].tag == "Enemy")
            {
                target = colliders[i].gameObject.transform;
                break;
            }
        }

        bouncesLeft--;
    }

    void Damage()
    {
        target.GetComponent<Enemy>().TakeDamage(damage);
        if(!bounces || bouncesLeft == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, splashRadius);
    }
}