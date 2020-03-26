using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Enemy
{
    public class EnemyBase : HealthSystem
    {
        [SerializeField] ParticleSystem characterParticle;
        [SerializeField] CircleCollider2D corpseCollider;

        protected override void Start()
        {
            base.Start();
            characterSprite.enabled = false;
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
            }
        }
    }
}

