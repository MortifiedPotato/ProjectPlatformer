using SoulHunter.Enemy;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour // Mort
{
    EnemyBase enemyBase;
    EnemyScript movement;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody;

    public Animator anim;

    private void Awake()
    {
        enemyBase = GetComponentInParent<EnemyBase>();
        movement = GetComponentInParent<EnemyScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        UpdateValues();
    }

    void UpdateValues()
    {
        // Syncs animator booleans with static booleans
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDead", false);
        anim.SetBool("isHurt", false);

        //anim.SetFloat("Speed", Mathf.Abs(" -- Movement Value --"));
    }
}
