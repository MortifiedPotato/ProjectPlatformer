using UnityEngine;
using SoulHunter.Player;

namespace SoulHunter.Gameplay
{
    public class HealthSystem : MonoBehaviour
    {
        const int maxHealth = 3;
        public int Health;

        public float deathHeight = -10;
        public float fade = 1;

        bool isDissolving;

        [SerializeField] SpriteRenderer characterSprite;

        private void Start()
        {
            //sprite = GetComponentInChildren<SpriteRenderer>();

            Health = maxHealth;
        }

        private void Update()
        {
            if (transform.position.y <= deathHeight)
            {
                isDissolving = true;
            }

            Dissolve();
            HandleDeath();
        }

        public void TakeDamage()
        {
            Health--;

            if (Health < 1)
            {
                isDissolving = true;
            }
        }

        void Healing()
        {
            Health++;
        }

        protected virtual void HandleDeath()
        {
            if (fade <= 0f)
            {
                if (!GetComponent<PlayerBase>())
                {
                    Destroy(gameObject);
                }
            }
        }

        void Dissolve()
        {
            if (isDissolving)
            {
                fade -= Time.deltaTime;
                if (fade <= 0f)
                {
                    fade = 0f;
                }

                characterSprite.material.SetFloat("_Fade", fade);
            }
            else
            {
                fade += Time.deltaTime;
                if (fade >= 1f)
                {
                    fade = 1f;
                }

                characterSprite.material.SetFloat("_Fade", fade);
            }
        }
    }
}