using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public float speed = 2.0f;
    public void Track (Transform targetPos)
    {
        target = targetPos;
    }

    
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceUpdate = speed * Time.deltaTime;

        if (direction.magnitude <= distanceUpdate)
        {
            //enemy hit
            Hit();
            return;
        }

        transform.Translate(direction.normalized * distanceUpdate, Space.World);

    }

    void Hit()
    {
        Debug.Log("projhit");
        Destroy(this.gameObject);
    }
}
