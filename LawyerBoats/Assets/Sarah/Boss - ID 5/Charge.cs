using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public float ChargeFor;
    float timerCharge = 0;
    public float TimeWornOut = 3;
    float WornOutTimer = 0;
    Patrol PatrolComp;
    public float IncreasedDamage = 1.5f;
    public float DecreaseDamage = 0.5f;
    bool IsCharging = false;
    bool IsWornOut = false;
    Enemy EnemyComp;


    // Start is called before the first frame update
    void Start()
    {
        PatrolComp = GetComponent<Patrol>();
        EnemyComp = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsCharging)
        {
            timerCharge += Time.deltaTime;
        }

        if(IsWornOut)
        {
            WornOutTimer += Time.deltaTime;
        }

        if (new Vector3(PatrolComp.PatrolTo.transform.position.x, this.transform.position.y, PatrolComp.PatrolTo.transform.position.z) == this.transform.position)
        {
            EnemyComp.shouldMove = false;
            IsWornOut = true;
            EnemyComp.DamageMultiplier = IncreasedDamage;
        }

        if(TimeWornOut <= WornOutTimer)
        {
            IsWornOut = false;
            EnemyComp.DamageMultiplier = DecreaseDamage;
            IsCharging = true;
        }

        if(timerCharge >=ChargeFor)
        {
            EnemyComp.DamageMultiplier = 1;
            IsCharging = false;
            timerCharge = 0;
            EnemyComp.shouldMove = true;
        }
    }
}
