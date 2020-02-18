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
            EnemyHealth.TakeDamage();
            
        }
    }
}
