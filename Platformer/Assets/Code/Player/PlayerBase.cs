using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Gameplay;

namespace SoulHunter.Player
{
    public class PlayerBase : HealthSystem
    {
        internal override void CheckDeath()
        {
            if (fade <= 0f)
            {
                SceneController.Instance.ResetScene();
            }
        }
    }
}