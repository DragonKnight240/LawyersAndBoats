using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    Rigidbody RB;
    BoxCollider Collider;
    internal Patrol PatrolComp;
    public int Damage = 2;
    public int Money;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, PatrolComp.PatrolTo.transform.position, Time.deltaTime * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Base"))
        {
            GameManager.Instance.loseHealth(Damage);
            GameManager.Instance.addMoney();
            Destroy(this.transform.parent.gameObject);
        }
    }
}
