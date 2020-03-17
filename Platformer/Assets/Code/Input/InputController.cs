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
        IMoveInput[] i_MoveInput;
        IAimInput i_AimInput;
        ITogglePause i_TogglePause;

        //Scripts
        PlayerMovement playerMovement;
        PlayerCombat playerCombat;
        GrappleSystem grappleSystem;

        private void Start()
        {
            //Get Interfaces
            i_MoveInput = GetComponents<IMoveInput>();
            i_AimInput = GetComponent<IAimInput>();
            i_TogglePause = UIManager.Instance.GameCanvas?.GetComponent<PauseMenu>();

            //Get Scripts
            playerMovement = GetComponent<PlayerMovement>();
            playerCombat = GetComponent<PlayerCombat>();
            grappleSystem = GetComponent<GrappleSystem>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            for (int i = 0; i < i_MoveInput.Length; i++)
            {
                i_MoveInput[i]?.HandleMoveInput(context.action.ReadValue<Vector2>());
            }
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
                i_TogglePause?.TogglePause();
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