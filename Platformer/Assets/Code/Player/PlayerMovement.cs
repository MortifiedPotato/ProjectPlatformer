using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter.Player
{
    public class PlayerMovement : MonoBehaviour, IMoveInput
    {
        [Header("Movement Status")]
        public bool isGrounded;
        public bool isSwinging;
        public bool isJumping;

        [Header("Movement Attributes")]
        public float speed = 3f;
        public float jumpSpeed = 8f;
        public float climbSpeed = 3f;
        public float swingForce = 4f;
        public float yankForce = 7f;

        public float scoreCountDown;
        public int Score;

        float fGroundedRememberTime = .25f;
        float fCutJumpHeight = .5f;
        float fGroundedRemember;

        [Header("Object Variables")]
        public SpriteRenderer playerSprite;

        [HideInInspector]
        public Vector2 ropeHook;

        Rigidbody2D rigidBody;

        Vector2 i_moveInput;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckForGround();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        public void HandleMovement()
        {
            if (i_moveInput.x < 0f || i_moveInput.x > 0f)
            {
                if (isSwinging)
                {
                    var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

                    Vector2 perpendicularDirection;
                    if (i_moveInput.x < 0)
                    {
                        perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                        var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                        Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                    }
                    else
                    {
                        perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                        var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                        Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                    }

                    var force = perpendicularDirection * swingForce;
                    rigidBody.AddForce(force, ForceMode2D.Force);
                }
                else
                {
                    if (isGrounded)
                    {
                        var groundForce = speed * 2f;
                        rigidBody.AddForce(new Vector2((i_moveInput.x * groundForce - rigidBody.velocity.x) * groundForce, 0));
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
                    }
                }
            }

            if (!isSwinging)
            {
                if (!isGrounded) return;

                if (isJumping)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                }
            }
        }

        public void Yank()
        {
            if (ropeHook != Vector2.zero)
            {
                rigidBody.AddForce((new Vector3(ropeHook.x, ropeHook.y, 0) - transform.position) * (yankForce * 10));
                GetComponent<GrappleSystem>().ResetRope();
            }
        }

        public void CutJump()
        {
            if (rigidBody.velocity.y > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * fCutJumpHeight);
            }

            isJumping = false;
        }

        void CheckForGround()
        {
            var halfHeight = playerSprite.bounds.extents.y;
            isGrounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f, 1);

            fGroundedRemember -= Time.deltaTime;

            if (isGrounded)
            {
                fGroundedRemember = fGroundedRememberTime;
            }
        }

        public void HandleMoveInput(Vector2 input)
        {
            i_moveInput = input;
        }
    }
}