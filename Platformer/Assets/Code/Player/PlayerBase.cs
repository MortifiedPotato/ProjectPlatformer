using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Player
{
    public class PlayerBase : HealthSystem
    {
        public static bool isPaused;

        protected override void Start()
        {
            base.Start();
            DespawnTimer = 1f;
        }

        protected override void Update()
        {
            base.Update();
            Dissolve();
        }

        void Dissolve()
        {
            characterSprite.material.SetFloat("_Fade", DespawnTimer);
        }

        protected override void HandleDeath()
        {
            SceneController.Instance.ResetScene();
        }
    }
}