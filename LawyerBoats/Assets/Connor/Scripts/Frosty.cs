using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frosty : MonoBehaviour
{
    public float slowPercentage = 30;
    public float defrostTimer = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if ((enemy.maxSpeed *= 1 - (slowPercentage / 100)) > enemy.Speed)
            {
                enemy.Speed = (enemy.maxSpeed *= 1 - (slowPercentage / 100)); // if there is a better slow don't slow.
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Defrosting();
            enemy.Speed = enemy.maxSpeed;
        }
    }

    void Defrosting()
    {
        float timer = 0;

        while (timer > defrostTimer)
        {
            timer += Time.deltaTime;
        }
    }
}
