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
                if (!hasBeenPlayed || repeatable)
                {
                    TriggerDialogue();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CancelDialogue();

                if (DialogueManager.Instance.sentences.Count == 0 && !repeatable)
                {
                    hasBeenPlayed = true;
                }
            }
        }
    }
}