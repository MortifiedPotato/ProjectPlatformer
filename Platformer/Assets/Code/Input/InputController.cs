﻿using UnityEngine.InputSystem;
using UnityEngine;

using SoulHunter.Player;

namespace SoulHunter.Input
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance;
        
        //Interfaces
        IMoveInput[] i_MoveInput;
        IAimInput i_AimInput;
        public ITogglePause i_TogglePause;

        //Scripts
        PlayerMovement playerMovement;
        PlayerBase playerBase;
        PlayerCombat playerCombat;
        GrappleSystem grappleSystem;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //Get Interfaces
            i_MoveInput = GetComponents<IMoveInput>();
            i_AimInput = GetComponent<IAimInput>();

            //Get Scripts
            playerMovement = GetComponent<PlayerMovement>();
            playerBase = GetComponent<PlayerBase>();
            playerCombat = GetComponent<PlayerCombat>();
            grappleSystem = GetComponent<GrappleSystem>();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.action.ReadValue<Vector2>() != Vector2.zero)
            {
                i_AimInput?.HandleAimInput(context.action.ReadValue<Vector2>());
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                for (int i = 0; i < i_MoveInput.Length; i++)
                {
                    i_MoveInput[i]?.HandleMoveInput(context.action.ReadValue<Vector2>());
                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (PlayerBase.isPaused)
            {
                return;
            }

            if (context.performed)
            {
                if (playerMovement)
                {
                    playerBase.isJumping = true;
                }
            }

            if (context.canceled)
            {
                playerMovement?.CutJump();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                GameManager.triggeringDialogue = true;
            }

            if (context.canceled)
            {
                GameManager.triggeringDialogue = false;
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (PlayerBase.isPaused)
            {
                return;
            }

            if (context.performed)
            {
                playerCombat?.Attack();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                i_TogglePause?.TogglePause();
            }
        }

        public void OnSwing(InputAction.CallbackContext context)
        {
            if (PlayerBase.isPaused)
            {
                return;
            }

            if (context.performed)
            {
                grappleSystem?.ShootGrapple(GetComponent<PlayerAim>().aimDirection);
            }
        }

        public void OnDetach(InputAction.CallbackContext context)
        {
            if (PlayerBase.isPaused)
            {
                return;
            }

            if (context.performed)
            {
                grappleSystem?.ResetRope();
            }
        }

        public void OnYank(InputAction.CallbackContext context)
        {
            if (PlayerBase.isPaused)
            {
                return;
            }

            if (context.performed)
            {
                playerMovement?.Yank();
            }
        }
    }
}