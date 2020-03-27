using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulHunter.Gameplay;

namespace SoulHunter.Weapons 
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] int KnockBack;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HealthSystem PlayerHealth = collision.gameObject.GetComponent<HealthSystem>();
                Rigidbody2D PlayerRB = collision.gameObject.GetComponent<Rigidbody2D>();
                PlayerRB.AddForce(transform.up * 100);
                Vector2 WeaponOwnerPos = transform.parent.gameObject.transform.position;
                if (WeaponOwnerPos.x < transform.position.x)
                {
                    PlayerRB.AddForce(Vector2.right * KnockBack);
                }
                else
                {
                    PlayerRB.AddForce(Vector2.left * KnockBack);
                }
                PlayerHealth.TakeDamage();
            }
        }
    }
}



