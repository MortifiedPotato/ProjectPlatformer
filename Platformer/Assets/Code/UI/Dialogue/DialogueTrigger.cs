using UnityEngine;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;
        bool hasBeenPlayed;

        public void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(dialogue);
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