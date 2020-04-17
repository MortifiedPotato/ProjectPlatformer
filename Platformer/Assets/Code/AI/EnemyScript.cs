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
        private SpriteRenderer RendererofSprites;
        private EnemyAnimation animationOfEnemy;

        float chngeDirTimer;

        public bool MoveRight;
        public bool Moving;

        public float fGroundedRememberTime = .25f;
        float fGroundedRemember;

        bool horizontalCollision;
        bool[] groundCollision = new bool[2];

        [SerializeField] LayerMask collisionLayer;
        [SerializeField] LayerMask groundCheckLayer;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            RendererofSprites = GetComponentInChildren<SpriteRenderer>();
            animationOfEnemy = GetComponentInChildren<EnemyAnimation>();
        }

        private void Update()
        {
            CheckForEdge();
            CheckForCollision();

            RandomDirectionChange();
        }

        void FixedUpdate()
        {
            if (!GetComponent<EnemyBase>().isDead)
            {
                if (Moving)
                {
                    animationOfEnemy.anim.SetFloat("Speed", Mathf.Abs(1));
                    if (MoveRight)
                    {
                        RendererofSprites.flipX = false;
                        transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        RendererofSprites.flipX = true;
                        transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
                    }
                }
            }
        }

        void CheckForEdge()
        {
            var halfHeight = GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
            groundCollision[0] = Physics2D.OverlapCircle(new Vector2(transform.position.x + 0.8f, transform.position.y - halfHeight), 0.15f, groundCheckLayer);
            groundCollision[1] = Physics2D.OverlapCircle(new Vector2(transform.position.x - 0.8f, transform.position.y - halfHeight), 0.15f, groundCheckLayer);

            fGroundedRemember -= Time.fixedDeltaTime;

            for (int i = 0; i < groundCollision.Length; i++)
            {
                if (groundCollision[i])
                {
                    fGroundedRemember = fGroundedRememberTime;
                    Moving = true;

                    return;
                }

                if (fGroundedRemember < 0 && !groundCollision[i])
                {
                    MoveRight = !MoveRight;
                }
            }
        }

        void CheckForCollision()
        {
            float collisionOffset = 1;
            if (MoveRight)
            {
                horizontalCollision = Physics2D.OverlapCircle(new Vector2(transform.position.x + collisionOffset, transform.position.y), 0.15f, collisionLayer);
            }
            else
            {
                horizontalCollision = Physics2D.OverlapCircle(new Vector2(transform.position.x - collisionOffset, transform.position.y), 0.15f, collisionLayer);
            }

            if (horizontalCollision)
            {
                MoveRight = !MoveRight;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var halfHeight = GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x + 0.8f, transform.position.y - halfHeight), .15f);
            Gizmos.DrawWireSphere(new Vector2(transform.position.x - 0.8f, transform.position.y - halfHeight), .15f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x - 1, transform.position.y), .15f);
            Gizmos.DrawWireSphere(new Vector2(transform.position.x + 1, transform.position.y), .15f);
        }
    }
}