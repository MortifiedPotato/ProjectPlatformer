using UnityEngine.InputSystem;
using UnityEngine;

using SoulHunter.Player;
using SoulHunter.Combat;
using SoulHunter.UI;

namespace SoulHunter.Input
{
    public class InputController : MonoBehaviour
    {
        //Interfaces
        IMoveInput i_MoveInput;
        IVerticalInput i_VerticalInput;
        IAimInput i_AimInput;

        //Scripts
        PlayerMovement playerMovement;
        PlayerCombat playerCombat;
        GrappleSystem grappleSystem;

        private void Awake()
        {
            //Get Interfaces
            i_MoveInput = GetComponent<IMoveInput>();
            i_VerticalInput = GetComponent<IVerticalInput>();
            i_AimInput = GetComponent<IAimInput>();

            //Get Scripts
            playerMovement = GetComponent<PlayerMovement>();
            playerCombat = GetComponent<PlayerCombat>();
            grappleSystem = GetComponent<GrappleSystem>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            i_MoveInput?.HandleMoveInput(context.action.ReadValue<Vector2>());
            i_VerticalInput?.VerticalMoveInput(context.action.ReadValue<Vector2>().y);
        }

        public void OnAimHorizontal(InputAction.CallbackContext context)
        {
            if (context.action.ReadValue<float>() != 0)
            {
                i_AimInput?.HandleAimInputX(context.action.ReadValue<float>());
            }
        }

        public void OnAimVertical(InputAction.CallbackContext context)
        {
            if (context.action.ReadValue<float>() != 0)
            {
                i_AimInput?.HandleAimInputY(context.action.ReadValue<float>());
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (playerMovement)
                {
                    playerMovement.isJumping = true;
                }
            }

            if (context.canceled)
            {
                playerMovement?.CutJump();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                playerCombat?.Attack();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                UIManager.Instance?.PauseGame();
            }
        }

        public void OnSwing(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                grappleSystem?.ShootGrapple(GetComponent<PlayerAim>().aimDirection);
            }
        }

        public void OnDetach(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                grappleSystem?.ResetRope();
            }
        }

        public void OnYank(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                playerMovement?.Yank();
            }
        }
    }
}