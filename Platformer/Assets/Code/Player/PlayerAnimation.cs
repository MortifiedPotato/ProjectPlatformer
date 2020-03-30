using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        PlayerMovement movement;
        PlayerCombat combat;

        Rigidbody2D rigidBody;

        SpriteRenderer playerSprite;
        Animator anim;

        [SerializeField] GameObject LeftSlash;
        [SerializeField] GameObject RightSlash;

        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            movement = GetComponent<PlayerMovement>();
            combat = GetComponent<PlayerCombat>();
            playerSprite = movement.playerSprite;
            anim = playerSprite.gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            UpdateValues();
            FlipTexture();
        }

        void UpdateValues()
        {
            anim.SetBool("isJumping", movement.isJumping);
            anim.SetBool("isSwinging", movement.isSwinging);
            anim.SetBool("isGrounded", movement.isGrounded);
            anim.SetBool("isAttacking", combat.isAttacking);

            if (movement.isGrounded && !PlayerBase.isPaused)
            {
                anim.SetFloat("Speed", Mathf.Abs(GetComponent<PlayerMovement>().i_moveInput.x));
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }
        }

        void FlipTexture()
        {
            if (movement.isGrounded && !PlayerBase.isPaused)
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
    }
}