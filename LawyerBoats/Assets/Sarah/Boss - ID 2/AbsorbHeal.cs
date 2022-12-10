using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbHeal : MonoBehaviour
{
    public float ActivatePercent = 0.5f;
    public float TimeToHeal = 2;
    float timerToHeal = 0;
    bool isActive = false;
    Enemy EnemyComp;
    Patrol PatrolComp;
    SphereCollider AbsorbRadius;
    public float RadiusAbsorb;
    List<Enemy> enemiesInRange;
    public int TimesCanActiviate = 1;
    int TimesHasActivated = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<Enemy>();
        AbsorbRadius = GetComponent<SphereCollider>();
        AbsorbRadius.radius = RadiusAbsorb;
        EnemyComp = GetComponentInParent<Enemy>();
        PatrolComp = GetComponentInParent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimesCanActiviate <= TimesHasActivated)
        {
            if (EnemyComp.Health / EnemyComp.MaxHealth <= ActivatePercent)
            {
                isActive = true;
                TimesHasActivated++;
            }
        }

        if(isActive)
        {
            timerToHeal += Time.deltaTime;

            if(timerToHeal >= TimeToHeal)
            {
                foreach(Enemy enemy in enemiesInRange)
                {
                    EnemyComp.IncreaseHealth(enemy.Health);
                    EnemyComp.Anim.SetTrigger("HealTrigger");
                    enemy.TakeDamage(enemy.Health);
                }
                isActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(!enemiesInRange.Contains(other.GetComponent<Enemy>()))
            {
                enemiesInRange.Add(other.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(other.GetComponent<Enemy>()))
            {
                enemiesInRange.Remove(other.GetComponent<Enemy>());
            }
        }
    }
}
