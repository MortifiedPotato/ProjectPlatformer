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
        [SerializeField] internal float MoveSpeed;
        [SerializeField] internal float KnockBack;
        [SerializeField] float flipDirectionTimer;

        float chngeDirTimer;

        public bool MoveRight;
        public bool Moving;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            KnockBack = KnockBack * 100;
        }

        private void Update()
        {
            RandomDirectionChange();
        }

        void FixedUpdate()
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
                //Vector2 difference = transform.position - other.transform.position;
                //rb.AddForce(difference * KnockBack);
                //rb.velocity = new Vector3(transform.position.x + difference.x, transform.position.y + difference.y, 0);
                //transform.position = new Vector2(transform.position.x + difference.x * Time.deltaTime, transform.position.y + difference.y * Time.deltaTime);
            }

            if (other.tag == "Edge")
            {
                rb.velocity = new Vector2(0, 0);
                MoveRight = !MoveRight;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
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