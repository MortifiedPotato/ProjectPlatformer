using UnityEngine.InputSystem;
using UnityEngine;

namespace SoulHunter.Player
{
    public class PlayerAim : MonoBehaviour, Input.IAimInput
    {
        [Header("Aim Variables")]
        public Vector2 aimDirection;
        public float aimAngle;
        public float crosshairDistance = 1f;

        Vector2 i_aimInput;

        private void Update()
        {
            HandleAim();
        }

        public void HandleAim()
        {
            if (GetComponent<PlayerInput>().currentControlScheme == "PC")
            {
                var v3 = UnityEngine.Input.mousePosition;
                v3.z = 10;
                var worldMousePosition = Camera.main.ScreenToWorldPoint(v3);
                var facingDirection = worldMousePosition - transform.position;
                aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
            }
            else
            {
                // Controller Aim Angle
                aimAngle = Mathf.Atan2(i_aimInput.y, i_aimInput.x);
                // Only the calculation here needs work
                // Vector 2 i_aimInput needs to be converted

                //https://forum.unity.com/threads/determining-rotation-and-converting-to-a-2d-direction-vector.416277/
                //https://answers.unity.com/questions/927323/how-to-get-smooth-analog-joystick-rotation-without.html
            }

            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        }

        public void HandleAimInputX(float input)
        {
            i_aimInput.x = input;
        }

        public void HandleAimInputY(float input)
        {
            i_aimInput.y = input;
        }
    }
}