using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        PlayerMovement movement;
        Rigidbody2D rigidBody;

        public SpriteRenderer playerSprite;
        Animator anim;

        [SerializeField] GameObject LeftSlash;
        [SerializeField] GameObject RightSlash;

        void Start()
        {
            rigidBody = GetComponentInParent<Rigidbody2D>();
            movement = GetComponentInParent<PlayerMovement>();
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
            anim.SetBool("isGrounded", PlayerBase.isGrounded);
            anim.SetBool("isAttacking", PlayerBase.isAttacking);
            anim.SetBool("isSwinging", PlayerBase.isSwinging);
            anim.SetBool("isJumping", PlayerBase.isJumping);
            anim.SetBool("isThrowing", PlayerBase.isThrowing);

            if (PlayerBase.isGrounded && !PlayerBase.isPaused)
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
            if (PlayerBase.isGrounded && !PlayerBase.isPaused)
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
            PlayerBase.isThrowing = false;
        }
    }
}