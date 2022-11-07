using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cHealth
{
    int currentHealth;
    int currentMaxHealth;

    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
        set
        {
            currentMaxHealth = value;
        }
    }

    public cHealth(int health, int maxHealth)
    {
        currentHealth = health;
        currentMaxHealth = maxHealth;
    }


    public void Damage(int dmgAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= dmgAmount;
        }
    }

}
