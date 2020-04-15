using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Enemy
{
    public class EnemyBase : Damageable
    {
        [SerializeField] ParticleSystem characterParticle;
        [SerializeField] CircleCollider2D corpseCollider;

        public SoulData soul;

        protected void Start()
        {
            GameManager.Instance.EnemyListRegistry(this);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();

            if (!isDead)
            {
                AudioManager.PlaySound(AudioManager.Sound.EnemyHurt, transform.position);
            }
            else
            {
                if (!immuneToDamage)
                {
                    immuneToDamage = true;

                    AudioManager.PlaySound(AudioManager.Sound.EnemyDeath, transform.position);
                    soul.SpawnParticle(transform.position);

                    for (int i = 0; i < GetComponents<Collider2D>().Length; i++)
                    {
                        GetComponents<Collider2D>()[i].enabled = false;
                    }
                    corpseCollider.enabled = true;

                    var emission = characterParticle.emission;
                    emission.enabled = false;
                }
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.EnemyListRegistry(this);
        }
    }
}

