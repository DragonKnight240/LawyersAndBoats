using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frosty : MonoBehaviour
{
    public float slowPercentage = 30;
    public float defrostTimer = 3;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.rigidbody.GetComponent<Enemy>();
            enemy.Speed *= slowPercentage;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.rigidbody.GetComponent<Enemy>();
            Defrosting();
            enemy.Speed = enemy.maxSpeed;
        }
    }

    private IEnumerator Defrosting()
    {
        yield return new WaitForSeconds(defrostTimer);
    }
}
