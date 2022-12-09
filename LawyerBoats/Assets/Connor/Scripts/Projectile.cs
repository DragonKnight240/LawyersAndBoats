using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private Vector3 lastPos;

    private int targetsZapped;

    public float speed = 2.0f; // proj speed
    public float splashRadius = 2.0f; // splash damage radius for explosives/chain effects

    //on hit effects
    public bool explosive = false;
    public bool electrifying = false;
    public bool bleed = false;
    public bool bounces = false;
    public bool disease = false;
    public bool stuns = false;
    public bool slows = false;

    public int electricTargets = 1; // additional targets to zap

    public int bouncesLeft = 3;

    [HideInInspector]
    public int damage; // tower collision damage

    public int bleedDamage; // Total bleed damage
    public int bleedTime; // Total bleed time
    public int bleedTick; // intervals between bleed damage;
    public float stunTime; // stun duration;
    public float slowTime; // slow duration;
    public float slowPercentage; // slow duration;
    private int bleedRemaining; // ticks left

    public void Track(Transform targetPos)
    {
        target = targetPos;
    }


    void Update()
    {
        if (target != null)
        {
            lastPos = target.position;
        }

        if (target == null)
        {
            if (!bounces)
            {
                Move(lastPos);
                if (transform.position == lastPos)
                {
                    if (explosive)
                    {
                        Explode();
                    }
                    Destroy(this.gameObject); // maybe add effect here
                }
            }
            return;
        }
        Debug.Log("premove");
        Move(target.position);
    }

    void Hit()
    {
        //maybe switch statement after more effects 
        if (explosive)
        {
            Explode();
        }
        else if (electrifying)
        {
            Damage();
            ChainLightning();
        }
        else if (bleed)
        {
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
        else if (disease)
        {
            //checkDisease();
            if (target.GetComponent<Enemy>().sick == false)
            {
                target.gameObject.AddComponent<Sickness>(); // stops dupe scripts but needs a way not to shoot
                target.GetComponent<Enemy>().sick = true;
            }

            Destroy(this.gameObject);
        }
        else if (stuns)
        {
            Stun();
            Damage();
        }
        else if (slows)
        {
            Slow();
            Damage();
        }
        else
        {
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

    void Stun()
    {
        target.gameObject.AddComponent<Stun>();
        target.gameObject.GetComponent<Stun>().stunTime = stunTime;
    }

    void Slow()
    {
        target.gameObject.AddComponent<Slowonhit>();
        target.gameObject.GetComponent<Slowonhit>().slowTime = slowTime;
        target.gameObject.GetComponent<Slowonhit>().slowPercentage = slowPercentage;
    }

    void Damage()
    {
        target.GetComponent<Enemy>().TakeDamage(damage);
        if(!bounces || bouncesLeft == 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    void Move(Vector3 tarpos)
    {
        Vector3 direction = tarpos - transform.position;
        float distanceUpdate = speed * Time.deltaTime;
        Debug.Log("pre hit");
        if (direction.magnitude <= distanceUpdate)
        {
            //enemy hit
            Hit();
            Debug.Log("posthit");
            return;
        }

        transform.Translate(direction.normalized * distanceUpdate, Space.World);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, splashRadius);
    }
}