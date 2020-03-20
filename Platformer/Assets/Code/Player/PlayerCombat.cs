using UnityEngine;

using SoulHunter.Player;
using SoulHunter.Gameplay;

namespace SoulHunter.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] float attackDuration;
        [SerializeField] BoxCollider2D Overlap;
        public GameObject Weapon;
        float attackTimer;
        bool attacking;

        void Start()
        {
            Weapon.SetActive(false);
            attackDuration = attackDuration / 10;
        }

        void Update()
        {
            if (attacking == true)
            {
                attackTimer += Time.deltaTime;
            }

            ResetWeapon();
        }

        public void Attack()
        {
            if (GetComponent<PlayerAim>().aimDirection.x < 0)
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
            if (attackTimer >= attackDuration)
            {
                Weapon.SetActive(false);
                attackTimer = 0;
                attacking = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyCombat>().Attack();
            }
        }
    }
}