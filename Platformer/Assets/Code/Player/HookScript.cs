using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    Rigidbody2D _rb;
    Collider2D _Coll;

    public HookStates hookState;

    [SerializeField] private GameObject Reticle;
    [SerializeField] private GameObject Player;
    [SerializeField] private int throwStrength;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _Coll = GetComponent<Collider2D>();
        hookState = HookStates.Inactive;
    }
    void Update()
    {
        HookSwitch();
        print(hookState);
    }

    void HookSwitch()
    {
        switch (hookState)
        {
            case HookStates.Inactive:
                transform.position = Player.transform.position;
                _Coll.enabled = false;
                _rb.gravityScale = 0;
                break;
            case HookStates.Aiming:
                transform.position = Player.transform.position;

                break;
            case HookStates.Throw:
                //Throw Hook
                _rb.AddForce((Reticle.transform.position - transform.position) * throwStrength);
                _rb.gravityScale = 1;
                _Coll.enabled = true;
                hookState = HookStates.Thrown;
                break;
            case HookStates.Thrown:

                break;
            case HookStates.Retrieve:
                _rb.constraints = RigidbodyConstraints2D.None;
                _rb.velocity = Vector3.zero;
                hookState = HookStates.Inactive;
                break;
            case HookStates.Hooked:
                _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                _rb.velocity = Vector3.zero;
                break;
            case HookStates.Climb:

                break;
            case HookStates.Yank:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            hookState = HookStates.Hooked;
        }
    }
}
