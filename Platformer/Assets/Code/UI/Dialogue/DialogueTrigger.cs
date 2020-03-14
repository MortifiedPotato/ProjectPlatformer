using UnityEngine;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public bool repeatable;
        public Color nameColor = Color.white;
        public Color dialogueColor = Color.white;

        public Dialogue dialogue;

        bool hasBeenPlayed;

        void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(dialogue, nameColor, dialogueColor);
        }

        void CancelDialogue()
        {
            DialogueManager.Instance.CancelDialogue();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (repeatable)
                {
                    TriggerDialogue();
                }
                else
                {
                    if (!hasBeenPlayed)
                    {
                        TriggerDialogue();
                        hasBeenPlayed = true;
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CancelDialogue();
            }
        }
    }
}