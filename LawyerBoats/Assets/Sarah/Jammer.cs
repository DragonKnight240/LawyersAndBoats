using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jammer : MonoBehaviour
{
    internal List<BaseTurret> JammedTurrets;
    public float JammingRadius;
    SphereCollider DamageCollision;
    Enemy enemy;
    float JammingTimer = 0;
    public float JammingTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        JammedTurrets = new List<BaseTurret>();
        DamageCollision.radius = JammingRadius;

        if(JammingTime > enemy.AnimationTimer)
        {
            JammingTime = enemy.AnimationTimer;
        }
    }

    private void Update()
    {
        if(!enemy.isAlive)
        {
            Jam();
            JammingTimer += Time.deltaTime;
            if(JammingTimer >= JammingTime)
            {
                Unjam();
            }
        }
    }

    void Jam()
    {
        foreach(BaseTurret turret in JammedTurrets)
        {
            turret.usesProjectile = false;
        }
    }

    void Unjam()
    {
        foreach (BaseTurret turret in JammedTurrets)
        {
            turret.usesProjectile = true;
        }
    }
}
