using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Enemy
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class EnemyScript : HealthSystem
    {
        Rigidbody2D rb;
        [SerializeField] internal float MoveSpeed;
        [SerializeField] internal float KnockBack;
        //[SerializeField] GameObject _GroundCheck;

        public bool MoveRight;
        public bool Moving;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            //_GroundCheck = gameObject.GetComponentsInChildren<CircleCollider2D>();
            KnockBack = KnockBack * 100;
           // FloorChecker = _GroundCheck.GetComponent<CircleCollider2D>();
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
}