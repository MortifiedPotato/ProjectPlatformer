using UnityEngine.InputSystem;
using UnityEngine;

//[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
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

    [Header("General Attributes")]
    public float respawnHeight = -10f;

    [Header("Aim Variables")]
    public Vector2 aimDirection;
    public float aimAngle;
    public float crosshairDistance = 1f;

    float fGroundedRememberTime = .25f;
    float fCutJumpHeight = .5f;
    float fGroundedRemember;

    [Header("Object Variables")]
    public SpriteRenderer playerSprite;
    [SerializeField] GameObject crosshair;

    [HideInInspector] public Vector2 ropeHook;

    Rigidbody2D rb;
    GrappleSystem grapple;

    Vector2 i_moveInput;
    Vector2 i_aimInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        grapple = GetComponent<GrappleSystem>();
    }

    private void Update()
    {
        CheckForGround();
        HandleAim();

        grapple.HandleRopeLength(i_moveInput.y);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleAim()
    {
        if (GetComponent<PlayerInput>().currentControlScheme == "PC")
        {
            var v3 = Input.mousePosition;
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

    void HandleMovement()
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
                rb.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                if (isGrounded)
                {
                    var groundForce = speed * 2f;
                    rb.AddForce(new Vector2((i_moveInput.x * groundForce - rb.velocity.x) * groundForce, 0));
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
        }

        if (!isSwinging)
        {
            if (!isGrounded) return;

            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
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

    // ----------------------------------------------------------------------------------------------------------------------------------
    // Input System Functions

    public void OnMove(InputAction.CallbackContext context)
    {
        i_moveInput = context.action.ReadValue<Vector2>();
    }

    public void OnAimHorizontal(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() != 0)
        {
            i_aimInput.x = context.action.ReadValue<float>();
        }
    }

    public void OnAimVertical(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() != 0)
        {
            i_aimInput.y = context.action.ReadValue<float>();
        }
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
            isJumping = true;
        }

        if (context.canceled)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fCutJumpHeight);
            }

            isJumping = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<PlayerScript>().Attack();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.UIManager.PauseGame();
        }
    }

    public void OnSwing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            grapple.ShootGrapple(aimDirection);
        }
    }

    public void OnDetach(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            grapple.ResetRope();
        }
    }

    public void OnYank(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ropeHook != Vector2.zero)
            {
                rb.AddForce((new Vector3(ropeHook.x, ropeHook.y, 0) - transform.position) * (yankForce * 10));
                grapple.ResetRope();
            }
        }
    }
}
