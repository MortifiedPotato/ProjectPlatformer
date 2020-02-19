using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed;
    public bool MoveRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (MoveRight)
        {
            rb.AddForce(new Vector2((+MoveSpeed), 0));
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.AddForce(new Vector2((-MoveSpeed), 0));
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }
}
