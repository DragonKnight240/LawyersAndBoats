using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : MonoBehaviour
{
    public int tickTime;
    public int ticksToDo;
    public int tickCount;
    bool ready = false;

    private float timer = 0;
    public int damage;

    void Start()
    {
    }


    void Update()
    {
        time();
        if (ready)
        {
            BleedTick(damage);
            ready = false;
        }

        if (ticksToDo <= tickCount)
        {
            gameObject.GetComponent<Enemy>().bleeding = false;
            Destroy(this);
        }
    }

    void BleedTick(int damage)
    {
        if (gameObject.GetComponent<Enemy>().Health > 0)
        {
            gameObject.GetComponent<Enemy>().TakeDamage(damage);
            tickCount++;
        }
        else
        {
            Destroy(this);
        }
    }

    void time()
    {
        timer += Time.deltaTime;

        if (timer > tickTime)
        {
            ready = true;
            timer = 0;
        }
    }
}
