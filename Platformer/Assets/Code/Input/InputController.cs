using UnityEngine.InputSystem;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float playerVelocity = 10f;
    public float jumpVelocity = 10f;

    public float aimSpeed = 10f;
    public float aimRadius;

    [SerializeField] GameObject AimReticle;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] GameObject Sickle;

    Rigidbody2D SickleRB;

    Vector2 i_movement;
    Vector2 i_aim;

    //public bool isGrounded;


    private void Start()
    {
        SickleRB = Sickle.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Aim();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aimRadius);
    }

    void Movement()
    {
        playerRB.velocity = (new Vector3(i_movement.x * (playerVelocity * Time.fixedDeltaTime), playerRB.velocity.y, 0));
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
            playerRB.velocity = (Vector3.up * jumpVelocity);
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
            print("I AIM");
        }
        if (context.canceled)
        {

            SickleRB.AddForce(AimReticle.transform.position - Sickle.transform.position);
            SickleRB.gravityScale = 1f;
        }
    }

    public void OnDetach(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

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
