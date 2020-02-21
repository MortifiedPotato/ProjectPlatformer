using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float MoveSpeed;
    HealthScript EnemyHealth;
    public bool MoveRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyHealth = GetComponent<HealthScript>();
    }

    void FixedUpdate()
    {
        if (MoveRight)
        {
            rb.velocity = (new Vector2(+MoveSpeed, 0));
        }
        else
        {
            rb.velocity = (new Vector2(-MoveSpeed, 0));
        }
    }
}
