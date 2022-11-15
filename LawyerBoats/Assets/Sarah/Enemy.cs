using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxSpeed;
    public float Speed;
    public int MaxHealth = 10;
    int Health;
    Rigidbody RB;
    BoxCollider Collider;
    internal Patrol PatrolComp;
    public int Damage = 2;
    public int Money;
    internal bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<BoxCollider>();
        Health = MaxHealth;
        PatrolComp = GetComponent<Patrol>();
        maxSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PatrolComp.PatrolTo.transform.position.x, transform.position.y, PatrolComp.PatrolTo.transform.position.z), Time.deltaTime * Speed);

            if (Health <= 0)
            {
                isAlive = false;
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Base"))
        {
            GameManager.Instance.loseHealth(Damage);
            Destroy(this.gameObject);
        }
    }
}
