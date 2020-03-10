using UnityEngine;

namespace SoulHunter.Player
{
    public interface IMoveInput
    {
        void HandleMoveInput(Vector2 moveInput);
    }
}