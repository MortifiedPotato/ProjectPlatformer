using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float attackduration;
    HealthScript healthscript;
    InputController inputscript;
    public GameObject Weapon;
    float attackTimer;
    bool attacking;

    void Start()
    {
        healthscript = GetComponent<HealthScript>();
        inputscript = GetComponent<InputController>();
        Weapon.SetActive(false);
        attackduration = attackduration / 10;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
        if (attacking == true)
        {
            attackTimer += Time.deltaTime;
        }
        ResetWeapon();
    }

    void Attack()
    {
        if (inputscript.aimDirection.x < 0)
        {
            Weapon.transform.localPosition = new Vector3(-1, 0, 0);
        }
        else
        {
            Weapon.transform.localPosition = new Vector3(1, 0, 0);
        }
        Weapon.SetActive(true);
        attacking = true;
    }

    void ResetWeapon()
    {
        if (attackTimer >= attackduration)
        {
            Weapon.SetActive(false);
            attackTimer = 0;
            attacking = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthscript.TakeDamage();
        }
    }
}
