using UnityEngine;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Color nameColor = Color.white;
        public Color dialogueColor = Color.white;

        public Dialogue dialogue;

        bool hasBeenPlayed;

        public void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(dialogue, nameColor, dialogueColor);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!hasBeenPlayed)
                {
                    TriggerDialogue();
                    hasBeenPlayed = true;
                }
            }
        }
    }
}