using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frosty : MonoBehaviour
{
    public float slowPercentage = 30;
    public float defrostTimer = 3;

    private void OnTriggerEnter(Collider other)
    {
        //print("oNsTART");
        if (other.gameObject.tag == "Enemy")
        {
            //print("Stay");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Speed *= 1 - (slowPercentage / 100);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("oNeXIT");
        if (other.gameObject.tag == "Enemy")
        {
            print("Exit");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Defrosting();
            enemy.Speed = enemy.maxSpeed;
        }
    }

    private IEnumerator Defrosting()
    {
        yield return new WaitForSeconds(defrostTimer);
    }
}
