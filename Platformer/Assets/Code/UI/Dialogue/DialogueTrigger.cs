using UnityEngine;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public bool isActivatable = true;
        public bool isRepeatable;

        public Color nameColor = Color.white;
        public Color dialogueColor = Color.white;

        public Dialogue dialogue;

        void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(dialogue, nameColor, dialogueColor);
        }

        void CancelDialogue()
        {
            DialogueManager.Instance.CancelDialogue();

            if (DialogueManager.Instance.sentences.Count == 0 && !isRepeatable)
            {
                isActivatable = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (isActivatable || isRepeatable)
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
            }
        }
    }
}