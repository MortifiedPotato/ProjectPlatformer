using UnityEngine.InputSystem;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("General Variables")]
    public float moveVelocity = 15f;
    public float jumpVelocity = 15f;

    public float aimSpeed = 10f;
    public float aimRadius = 2;


    [Header("Movement Variables")]
    [SerializeField]
    public float fGroundedRememberTime = .25f;
    float fGroundedRemember;

    [SerializeField]
    float fHorizontalAcceleration = 1;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = .5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;

    [Range(.1f, 1)] public float fCutJumpHeight = .5f;

    [Header("Object Variables")]
    [SerializeField] GameObject AimReticle;
    [SerializeField] GameObject Sickle;

    [Header("Component Variables")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] HookScript hook;

    Vector2 i_movement;
    Vector2 i_aim;

    [Header("")]
    public bool isGrounded;

    private void Update()
    {
        Aim();

        fGroundedRemember -= Time.deltaTime;

        if (isGrounded)
        {
            fGroundedRemember = fGroundedRememberTime;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //rigid.velocity = (new Vector3(i_movement.x * (moveVelocity * Time.fixedDeltaTime), rigid.velocity.y, 0));

        float fHorizontalVelocity = rigid.velocity.x;
        fHorizontalVelocity += i_movement.x;

        if (Mathf.Abs(i_movement.x) < 0.0f)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.fixedDeltaTime * 10f);
        }
        else if (Mathf.Sign(i_movement.x) != Mathf.Sign(fHorizontalVelocity))
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.fixedDeltaTime * 10f);
        }
        else
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.fixedDeltaTime * 10f);
        }

        //rigid.velocity = (new Vector2(fHorizontalVelocity * (moveVelocity * Time.fixedDeltaTime), rigid.velocity.y));
        rigid.AddForce(new Vector2(fHorizontalVelocity * (moveVelocity * Time.fixedDeltaTime), rigid.velocity.y));
    }

    void Aim()
    {
        if (GetComponent<PlayerInput>().currentControlScheme == "PC")
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AimReticle.transform.localPosition = new Vector3(mousePos.x, mousePos.y);
        }
        else
        {
            AimReticle.transform.position += new Vector3(i_aim.x * (aimSpeed * Time.deltaTime), i_aim.y * (aimSpeed * Time.deltaTime), 0);
        }

        Vector3 offset = AimReticle.transform.localPosition - transform.localPosition;
        offset = offset.normalized * aimRadius;
        AimReticle.transform.localPosition = offset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aimRadius);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }
    // ----------------------------------------------------------------------------------------------------------------------------------
    // Input System Functions

    public void OnMove(InputAction.CallbackContext context)
    {
        i_movement = context.action.ReadValue<Vector2>() * 50;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        i_aim = context.action.ReadValue<Vector2>();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded || fGroundedRemember > 0)
            {
                rigid.velocity = (new Vector3(rigid.velocity.x, jumpVelocity, 0));
                fGroundedRemember = 0f;
                isGrounded = false;
            }
        }

        if (context.canceled)
        {
            if (rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * fCutJumpHeight);
            }
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void OnCycleUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void OnCycleDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void OnSwing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (hook.hookState == HookStates.Inactive)
            {
                hook.hookState = HookStates.Aiming;
            }
        }
        if (context.canceled)
        {
            if (hook.hookState == HookStates.Aiming)
            {
                hook.hookState = HookStates.Throw;
            }

        }
    }

    public void OnDetach(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hook.hookState = HookStates.Retrieve;
        }
    }

    public void OnPull(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hook.hookState = HookStates.Climb;
        }
    }

    public void OnYank(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hook.hookState = HookStates.Yank;
        }
    }
}
