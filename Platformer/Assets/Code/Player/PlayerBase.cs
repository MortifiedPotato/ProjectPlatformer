using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Player
{
    public class PlayerBase : HealthSystem
    {
        protected override void HandleDeath()
        {
            if (fade <= 0f)
            {
                Base.SceneController.Instance.ResetScene();
            }
        }
    }
}