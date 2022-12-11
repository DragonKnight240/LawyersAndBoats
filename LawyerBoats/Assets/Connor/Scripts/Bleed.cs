using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : MonoBehaviour
{
    public int tickTime = 1;
    public int ticksToDo = 0;
    public int tickCount = 0;
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
        gameObject.GetComponent<Enemy>().TakeDamage(damage);
        tickCount++;
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
