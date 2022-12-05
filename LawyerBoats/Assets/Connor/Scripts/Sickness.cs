using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickness : MonoBehaviour
{
    public float spreadTime = 3;
    public int spreadRadius = 11;
    public int damageTick = 1;
    bool ready = false;

    private float timer = 0;

    //void Awake()
    //{

    //}

    void Start()
    {

    }


    void Update()
    {
        time();

        if (ready)
        {
            Debug.Log("works");
            Decay(damageTick);
            Pandemic();
            ready = false;
        }
    }

    void Pandemic()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spreadRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy" && colliders[i].GetComponent<Enemy>().sick == false)
            {
                colliders[i].gameObject.AddComponent<Sickness>();
                colliders[i].GetComponent<Enemy>().sick = true;
            }
        }
    }

    void Decay(int damage)
    {
        gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, spreadRadius);
    }

    void time()
    {
        timer += Time.deltaTime;

        if (timer > spreadTime)
        {
            ready = true;
            timer = 0;
        }
    }
}
