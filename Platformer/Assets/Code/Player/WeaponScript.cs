using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HealthScript EnemyHealth = collision.gameObject.GetComponent<HealthScript>();
            Rigidbody2D EnemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
            EnemyRB.AddForce(transform.up * 200);
            EnemyHealth.TakeDamage();
            
        }
    }
}
