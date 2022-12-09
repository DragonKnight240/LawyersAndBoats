using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowonhit : MonoBehaviour
{
    public float timer;
    [HideInInspector]
    public float slowTime;
    [HideInInspector]
    public float slowPercentage;

    private Enemy enemy;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemy.Speed = (enemy.maxSpeed * (1 - (slowPercentage / 100)));
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= slowTime)
        {
            enemy.Speed = enemy.maxSpeed;
            Destroy(this);
        }

    }
}
