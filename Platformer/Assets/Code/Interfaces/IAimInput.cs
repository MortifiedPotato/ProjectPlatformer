using UnityEngine;

namespace SoulHunter.Player
{
    public interface IAimInput
    {
        void HandleAimInputX(float inputX);
        void HandleAimInputY(float inputY);
    }
}