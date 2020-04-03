using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        PlayerMovement movement;
        PlayerBase playerBase;

        Rigidbody2D rigidBody;

        public SpriteRenderer playerSprite;
        Animator anim;

        [SerializeField] GameObject LeftSlash;
        [SerializeField] GameObject RightSlash;

        public RaycastHit2D hit;

        void Start()
        {
            rigidBody = GetComponentInParent<Rigidbody2D>();
            movement = GetComponentInParent<PlayerMovement>();
            playerBase = GetComponentInParent<PlayerBase>();
            playerSprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            UpdateValues();
            FlipTexture();
        }

        void UpdateValues()
        {
            anim.SetBool("isGrounded", playerBase.isGrounded);
            anim.SetBool("isAttacking", playerBase.isAttacking);
            anim.SetBool("isSwinging", playerBase.isSwinging);
            anim.SetBool("isJumping", playerBase.isJumping);
            anim.SetBool("isThrowing", playerBase.isThrowing);

            if (playerBase.isGrounded && !PlayerBase.isPaused)
            {
                anim.SetFloat("Speed", Mathf.Abs(movement.i_moveInput.x));
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }
        }

        void FlipTexture()
        {
            if (playerBase.isGrounded && !PlayerBase.isPaused)
            {
                if (movement.i_moveInput.x < 0)
                {
                    // Direction Left
                    playerSprite.flipX = true;
                    LeftSlash.SetActive(true);
                    RightSlash.SetActive(false);
                }
                else if (movement.i_moveInput.x > 0)
                {
                    // Direction Right
                    playerSprite.flipX = false;
                    LeftSlash.SetActive(false);
                    RightSlash.SetActive(true);
                }
            }
            else
            {
                if (rigidBody.velocity.x < 0)
                {
                    playerSprite.flipX = true;
                }
                else if (rigidBody.velocity.x > 0)
                {
                    playerSprite.flipX = false;
                }
            }
        }

        public void FlipSlashSprites()
        {
            RightSlash.GetComponent<SpriteRenderer>().flipY = !RightSlash.GetComponent<SpriteRenderer>().flipY;
            LeftSlash.GetComponent<SpriteRenderer>().flipY = !LeftSlash.GetComponent<SpriteRenderer>().flipY;
        }

        public void ResetThrowing()
        {
            playerBase.isThrowing = false;
        }

        public void InitiateGrapple()
        {
            GetComponentInParent<GrappleSystem>().InitiateGrapple(hit);
        }
    }
}