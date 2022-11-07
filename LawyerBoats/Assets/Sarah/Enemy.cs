using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public int MaxHealth;
    int Health;
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
        Health = MaxHealth;
        PatrolComp = GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PatrolComp.PatrolTo.transform.position.x, transform.position.y, PatrolComp.PatrolTo.transform.position.z), Time.deltaTime * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Base"))
        {
            GameManager.Instance.loseHealth(Damage);
            Destroy(this.gameObject);
        }
    }
}
