using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Enemy
{
    public class EnemyBase : Damageable
    {
        [SerializeField] ParticleSystem characterParticle;
        [SerializeField] CircleCollider2D corpseCollider;

        protected void Start()
        {
            //characterSprite.enabled = false;
            GameManager.Instance.EnemyListRegistry(this);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();

            if (isDead)
            {
                for (int i = 0; i < GetComponents<Collider2D>().Length; i++)
                {
                    GetComponents<Collider2D>()[i].enabled = false;
                }
                corpseCollider.enabled = true;

                var emission = characterParticle.emission;
                emission.enabled = false;

                AudioManager.PlaySound(AudioManager.Sound.EnemyDeath, transform.position);
            }
            else
            {
                AudioManager.PlaySound(AudioManager.Sound.EnemyHurt, transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (!isDead)
                {
                    return;
                }
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.EnemyListRegistry(this);
        }
    }
}

