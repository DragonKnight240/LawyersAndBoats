using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public float timer;
    [HideInInspector]
    public float stunTime;
    private Enemy enemy;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemy.Speed = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stunTime)
        {
            enemy.Speed = enemy.maxSpeed;
            Destroy(this);
        }

    }
}
