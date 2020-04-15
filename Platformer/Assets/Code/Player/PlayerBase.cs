using UnityEngine;

using SoulHunter.UI;
using SoulHunter.Gameplay;

namespace SoulHunter.Player
{
    public class PlayerBase : Damageable // Mort
    {
        GameUI healthUI;

        // Player states
        public static bool isPaused;
        public static bool isTeleporting;

        public static bool isGrounded;
        public static bool isAttacking;
        public static bool isSwinging;
        public static bool isJumping;
        public static bool isThrowing;

        // Teleportation coordinates
        public static Transform teleportDestination;

        protected void Start()
        {
            DespawnTimer = 1f;
            healthUI = FindObjectOfType<GameUI>();
        }

        protected override void Update()
        {
            base.Update();
            Dissolve();

            if (isTeleporting || isDead)
            {
                DespawnTimer -= Time.deltaTime;
                if (DespawnTimer <= 0f)
                {
                    DespawnTimer = 0f;
                    if (!isDead)
                    {
                        Teleport();
                    }
                }
            }
            else
            {
                DespawnTimer += Time.deltaTime;
                if (DespawnTimer >= 1f)
                {
                    DespawnTimer = 1f;
                }
                else
                {
                    AudioManager.PlaySound(AudioManager.Sound.TeleportAppear, transform.position);
                }
            }
        }

        /// <summary>
        /// Syncs shader value with despawn value
        /// </summary>
        void Dissolve()
        {
            characterSprite.material.SetFloat("_Fade", DespawnTimer);
        }

        /// <summary>
        /// Teleports player to the next destination
        /// </summary>
        void Teleport()
        {
            transform.position = teleportDestination.position;
            isTeleporting = false;
            isPaused = false;
        }

        /// <summary>
        /// Overrides default TakeDamage value to update UI and play unique sounds
        /// </summary>
        public override void TakeDamage()
        {
            base.TakeDamage();

            if (!isDead)
            {
                AudioManager.PlaySound(AudioManager.Sound.PlayerHurt, transform.position);
            }
            else
            {
                AudioManager.PlaySound(AudioManager.Sound.PlayerDeath, transform.position);
            }

            healthUI.UpdateHealthPanel(Health);
        }

        /// <summary>
        /// Handles player death by restarting scene
        /// </summary>
        protected override void HandleDeath()
        {
            if (isDead && !isTeleporting)
            {
                SceneController.Instance.ResetScene();
            }
        }
    }
}