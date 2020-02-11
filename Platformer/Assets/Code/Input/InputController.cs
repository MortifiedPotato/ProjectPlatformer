using UnityEngine.InputSystem;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;

    public float aimSpeed = 10f;
    public float maxAimDist = 5f;

    [SerializeField] GameObject AimReticle;

    Vector2 i_movement;
    Vector2 i_aim;

    float aimDist;

    private void Update()
    {
        aimDist = Vector3.Distance(transform.position, AimReticle.transform.position);

        transform.position += new Vector3(i_movement.x * (moveSpeed * Time.deltaTime), 0, 0);

        if (aimDist < maxAimDist)
        {
            AimReticle.transform.position += new Vector3(i_aim.x * (aimSpeed * Time.deltaTime), i_aim.y * (aimSpeed * Time.deltaTime), 0);
        }
        else
        {
            AimReticle.transform.position = Vector2.MoveTowards(AimReticle.transform.position, transform.position, .5f * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        i_movement = context.action.ReadValue<Vector2>();
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

    public void OnResetAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AimReticle.transform.localPosition = Vector3.zero;
            i_aim = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * (jumpSpeed * 80));
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
