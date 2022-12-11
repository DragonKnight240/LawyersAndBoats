using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppingNotPatrol : MonoBehaviour
{
    public float StopEverySeconds = 3;
    float StopEveryTimer = 0;
    public float StopFor = 3;
    float StopForTimer = 0;
    Patrol PatrolComp;
    Enemy EnemyComp;
    bool isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        PatrolComp = GetComponent<Patrol>();
        EnemyComp = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStopped)
        {
            StopForTimer += Time.deltaTime;

            if(StopFor <= StopForTimer)
            {
                EnemyComp.Anim.SetTrigger("StopTrigger");
                EnemyComp.shouldMove = true;
                isStopped = false;
                StopForTimer = 0;
            }
        }
        else
        {
            StopEveryTimer += Time.deltaTime;

            if(StopEveryTimer >= StopEverySeconds)
            {
                EnemyComp.shouldMove = false;
                isStopped = true;
                StopEveryTimer = 0;
            }
        }
    }
}
