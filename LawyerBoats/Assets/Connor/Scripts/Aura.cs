using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    public float damageAmpPercentage = 1.33f;
    public float atkSpdAmpPercentage = 1.33f;
  
    public float auraRadius = 25;

    public bool damageAura = false;
    public bool atkSpdAura = false;

    public UpgradeSystem.BaseTowerNames BaseTower;
    public UpgradeSystem.BaseTowerNames TowerName;

    public int Cost;

    void Start()
    {
        if (damageAura || atkSpdAura)
        {
            InvokeRepeating("checkBuffs", 0f, 0.5f);
        }
    }

    void checkBuffs()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, auraRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Tower")
            {
                BaseTurret turret = collider.gameObject.GetComponent<BaseTurret>();

                if (damageAura)
                {
                    if (turret.baseDamage == turret.damage)
                    {
                        turret.damage = Mathf.RoundToInt(turret.damage * damageAmpPercentage);
                    }
                }
                if (atkSpdAura)
                {
                    if (turret.baseFireRate == turret.fireRate)
                    {
                        turret.fireRate = turret.fireRate * atkSpdAmpPercentage;
                    }
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }

}
