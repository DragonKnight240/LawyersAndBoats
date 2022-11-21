using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEnemy : MonoBehaviour
{
    Enemy enemy;
    public float TimeForIncreasedDamage = 5;
    public float DamageMultipledBy = 2;
    bool IncreasedDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IncreaseDamage()
    {
        GameManager.Instance.DamageMultiplier = DamageMultipledBy;
        GameManager.Instance.TimeForMultiplier = TimeForIncreasedDamage;
        GameManager.Instance.UsingMultiplier = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Base"))
        {
            IncreaseDamage();
            print("Increase Damage");
        }
    }
}
