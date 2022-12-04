using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielding : MonoBehaviour
{
    Enemy EnemyComp;
    Patrol PatrolComp;
    internal int ShieldStrength = 100;
    float TimerStagged = 0;
    public float TimeStaggered = 5;
    bool isStaggered = false;
    public float IncreasedDamage = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        EnemyComp = GetComponent<Enemy>();
        PatrolComp = GetComponent<Patrol>();

        EnemyComp.isShielded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShieldStrength <= 0 && EnemyComp.isShielded)
        {
            EnemyComp.shouldMove = false;
            EnemyComp.isShielded = false;
            isStaggered = true;
            EnemyComp.DamageMultiplier = IncreasedDamage;
        }

        if(isStaggered)
        {
            TimerStagged += Time.deltaTime;

            if(TimerStagged >= TimeStaggered)
            {
                EnemyComp.shouldMove = true;
                EnemyComp.DamageMultiplier = 1;
                isStaggered = false;
                TimerStagged = 0;
            }
        }
    }
}
