using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Enemy
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class EnemyScript : MonoBehaviour
    {
        Rigidbody2D rb;
        [SerializeField] protected float MoveSpeed;
        [SerializeField] float flipDirectionTimer;

        float chngeDirTimer;

        public bool MoveRight;
        public bool Moving;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            RandomDirectionChange();
        }

        void FixedUpdate()
        {
            if (!GetComponent<EnemyBase>().isDead)
            {
                if (Moving)
                {
                    if (MoveRight)
                    {
                        transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
                    }
                }
            }
            
        }

        void RandomDirectionChange()
        {
            chngeDirTimer += Time.deltaTime;
            if (chngeDirTimer >= flipDirectionTimer)
            {
                rb.velocity = new Vector2(0, 0);
                MoveRight = !MoveRight;
                flipDirectionTimer = Random.Range(2, 6);
                chngeDirTimer = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "P_weapon")
            {
                Moving = false;
            }

            if (other.tag == "Edge")
            {
                rb.velocity = new Vector2(0, 0);
                MoveRight = !MoveRight;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag == "Environment")
            {
                Moving = true;
            }
            else
            {
                Moving = false;
            }
        }
    }
}