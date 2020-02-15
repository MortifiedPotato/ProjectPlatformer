﻿using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [Header("General Variables")]
    public bool isGrounded;
    public bool isSwinging;
    public bool isJumping;

    public float respawnHeight = -10f;

    public float speed = 1f;
    public float jumpSpeed = 3f;
    public float climbSpeed = 3f;

    public Vector2 aimDirection;
    public float aimDistance = 1f;
    public float aimAngle;

    [Header("Movement Variables")]
    [SerializeField]
    public float fGroundedRememberTime = .25f;
    float fGroundedRemember;

    [Range(.1f, 1)] public float fCutJumpHeight = .5f;

    [Header("Object Variables")]
    [SerializeField] GameObject AimReticle;

    [Header("Component Variables")]
    Rigidbody2D rb;
    GrappleSystem grapple;

    public SpriteRenderer playerSprite;
    public Vector2 ropeHook;
    public float swingForce = 4f;

    Vector2 i_moveInput;
    Vector2 i_aimInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        grapple = GetComponent<GrappleSystem>();
    }

    private void Update()
    {
        CheckRespawnHeight();
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
            var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            var facingDirection = worldMousePosition - transform.position;
            aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        }
        else
        {
            aimAngle = Mathf.Atan2(i_aimInput.y, i_aimInput.x);
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
        isGrounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);

        fGroundedRemember -= Time.deltaTime;

        if (isGrounded)
        {
            fGroundedRemember = fGroundedRememberTime;
        }
    }

    void CheckRespawnHeight()
    {
        if (transform.position.y <= respawnHeight)
        {
            GameManager.Instance.SceneController.ResetScene();
        }
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
    // Input System Functions

    public void OnMove(InputAction.CallbackContext context)
    {
        i_moveInput = context.action.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        i_aimInput = context.action.ReadValue<Vector2>();
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

    public void OnPull(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        }
    }

    public void OnYank(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        }
    }
}
