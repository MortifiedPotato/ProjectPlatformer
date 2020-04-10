using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Player
{
    public class PlayerBase : Damageable // Mort
    {
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

        protected override void Start()
        {
            base.Start();
            DespawnTimer = 1f;
        }

        protected override void Update()
        {
            base.Update();
            Dissolve();

            if (isTeleporting)
            {
                DespawnTimer -= Time.deltaTime;
                if (DespawnTimer <= 0f)
                {
                    DespawnTimer = 0f;
                    Teleport();
                }
            }
            else
            {
                DespawnTimer += Time.deltaTime;
                if (DespawnTimer >= 1f)
                {
                    DespawnTimer = 1f;
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
        }

        /// <summary>
        /// Handles player death by restarting scene
        /// </summary>
        protected override void HandleDeath()
        {
            SceneController.Instance.ResetScene();
        }
    }
}