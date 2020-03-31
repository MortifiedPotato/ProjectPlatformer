using UnityEngine;

using SoulHunter.Enemy;

namespace SoulHunter.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        PlayerBase playerBase;

        [SerializeField] float attackDuration;
        [SerializeField] BoxCollider2D Overlap;
        public GameObject Weapon;
        float attackTimer = 2f;

        void Start()
        {
            Weapon.SetActive(false);
            attackDuration = attackDuration / 10;

            playerBase = GetComponent<PlayerBase>();
        }

        void Update()
        {
            if (playerBase.isAttacking == true)
            {
                attackTimer += Time.deltaTime;
            }

            ResetWeapon();
        }

        public void Attack()
        {
            if (playerBase.isSwinging || playerBase.isAttacking)
            {
                return;
            }

            if (GetComponent<PlayerAim>().aimDirection.x < 0)
            {
                // Attack Left
                Weapon.transform.localPosition = new Vector3(-1, 0, 0);
            }
            else
            {
                //Attack Right
                Weapon.transform.localPosition = new Vector3(1, 0, 0);
            }
            GetComponent<PlayerAnimation>().FlipSlashSprites();
            Weapon.SetActive(true);
            playerBase.isAttacking = true;
        }

        void ResetWeapon()
        {
            if (attackTimer >= attackDuration)
            {
                Weapon.SetActive(false);
                attackTimer = 0;
                playerBase.isAttacking = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (!other.GetComponent<EnemyBase>().isDead)
                {
                    other.GetComponent<EnemyCombat>().Attack();
                }
            }
        }
    }
}