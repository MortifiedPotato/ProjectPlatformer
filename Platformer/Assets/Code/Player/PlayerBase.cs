using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Player
{
    public class PlayerBase : HealthSystem
    {
        public static bool isPaused;
        public bool isTeleporting;

        public bool isGrounded;
        public bool isAttacking;
        public bool isSwinging;
        public bool isJumping;
        public bool isThrowing;

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
        }

        void Dissolve()
        {
            characterSprite.material.SetFloat("_Fade", DespawnTimer);
        }

        void Teleport()
        {
            SceneController.Instance.TransitionScene(0);
        }

        protected override void HandleDeath()
        {
            SceneController.Instance.ResetScene();
        }
    }
}