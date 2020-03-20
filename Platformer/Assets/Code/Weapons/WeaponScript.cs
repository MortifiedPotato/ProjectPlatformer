using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Weapons
{
    public class WeaponScript : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.CompareTag("Enemy"))
            {
                HealthSystem EnemyHealth = collision.gameObject.GetComponent<HealthSystem>();
                Rigidbody2D EnemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
                EnemyRB.AddForce(transform.up * 200);
                EnemyHealth.TakeDamage();
            }
        }
    }
}