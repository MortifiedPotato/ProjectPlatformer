using UnityEngine;

namespace SoulHunter.Gameplay
{
    public class Damageable : MonoBehaviour
    {
        const int maxHealth = 3;
        public int Health;

        public bool isDead;

        [SerializeField] protected float expirationHeight = -10;
        [SerializeField] protected float DespawnTimer = 10;

        [SerializeField] protected SpriteRenderer characterSprite;

        protected virtual void Start()
        {
            Health = maxHealth;
        }

        protected virtual void Update()
        {
            HandleFallDeath();

            HandleDespawn();

            if (isDead && DespawnTimer <= 0f)
            {
                HandleDeath();
            }
        }

        public virtual void TakeDamage()
        {
            Health--;

            if (Health < 1)
            {
                isDead = true;
            }
        }

        void HandleFallDeath()
        {
            if (transform.position.y <= expirationHeight)
            {
                isDead = true;
            }
        }

        void HandleDespawn()
        {
            if (isDead)
            {
                DespawnTimer -= Time.deltaTime;
                if (DespawnTimer <= 0f)
                {
                    DespawnTimer = 0f;
                }
            }
        }

        protected virtual void HandleDeath()
        {
            Destroy(gameObject);
        }
    }
}