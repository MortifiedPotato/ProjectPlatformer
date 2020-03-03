using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float MoveSpeed;
    [SerializeField] float KnockBack;
    HealthScript EnemyHealth;
    public bool MoveRight;
    public bool Moving;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyHealth = GetComponent<HealthScript>();
        KnockBack = KnockBack * 100;
    }

    void FixedUpdate()
    {
        if (Moving)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "P_weapon")
        {
            Moving = false;
            Vector2 difference = transform.position - other.transform.position;
            rb.AddForce(difference * KnockBack);
            //rb.velocity = new Vector3(transform.position.x + difference.x, transform.position.y + difference.y, 0);
            //transform.position = new Vector2(transform.position.x + difference.x * Time.deltaTime, transform.position.y + difference.y * Time.deltaTime);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Environment")
        {
            Moving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Environment")
        {
            Moving = true;
        }
    }
}
