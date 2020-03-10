using UnityEngine;

namespace SoulHunter.Gameplay
{
    public class HealthSystem : MonoBehaviour
    {
        public int HealthPoints = 3;
        public bool isDissolving;
        SpriteRenderer sprite;
        public float deathHeight = -10;
        internal float fade = 1;

        private void Start()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            if (transform.position.y <= deathHeight)
            {
                isDissolving = true;
            }

            Dissolve();
            CheckDeath();
        }

        public void TakeDamage()
        {
            HealthPoints--;

            if (HealthPoints < 1)
            {
                isDissolving = true;
            }
        }

        void Healing()
        {
            HealthPoints++;
        }

        internal virtual void CheckDeath()
        {
            if (fade <= 0f)
            {
                Destroy(gameObject);
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

                sprite.material.SetFloat("_Fade", fade);
            }
            else
            {
                fade += Time.deltaTime;
                if (fade >= 1f)
                {
                    fade = 1f;
                }

                sprite.material.SetFloat("_Fade", fade);
            }
        }
    }
}