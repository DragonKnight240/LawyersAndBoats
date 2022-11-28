using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : MonoBehaviour
{
    GameObject ShieldedEnemy;
    public int ShieldMaxHealth;
    internal int ShieldHealth;
    Collider ShielderRange;
    List<Enemy> InRangeEnemies;
    public float shieldedCooldown = 5;
    float ShieldedTimer = 0;
    bool OnCooldownShield = false;

    // Start is called before the first frame update
    void Start()
    {
        InRangeEnemies = new List<Enemy>();
        ShielderRange = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Enemy>().isAlive)
        {
            if (OnCooldownShield)
            {
                if (ShieldedEnemy == null)
                {
                    if (InRangeEnemies.Count != 0)
                    {
                        ShieldedEnemy = InRangeEnemies[0].gameObject;
                        ShieldedEnemy.GetComponent<Enemy>().isShielded = true;
                    }
                }

                if (ShieldHealth <= 0)
                {
                    ShieldedEnemy.GetComponent<Enemy>().isShielded = false;
                    OnCooldownShield = true;
                }
            }
            else
            {
                ShieldedTimer += Time.deltaTime;

                if (ShieldedTimer >= shieldedCooldown)
                {
                    ShieldedTimer = 0;
                    OnCooldownShield = false;
                    ShieldHealth = ShieldMaxHealth;
                }
            }
        }
        else
        {
            if (ShieldedEnemy != null)
            {
                ShieldedEnemy.GetComponent<Enemy>().isShielded = false;
                ShieldedEnemy = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if (!InRangeEnemies.Contains(other.GetComponent<Enemy>()))
            {
                InRangeEnemies.Add(other.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (InRangeEnemies.Contains(other.GetComponent<Enemy>()))
            {
                InRangeEnemies.Remove(other.GetComponent<Enemy>());
            }
        }
    }
}
