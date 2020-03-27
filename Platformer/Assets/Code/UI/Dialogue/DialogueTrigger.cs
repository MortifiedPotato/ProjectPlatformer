using System.Collections;
using UnityEngine;

using SoulHunter.Player;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public bool isActivatable = true;
        public bool isRepeatable;

        public Color nameColor = Color.white;
        public Color dialogueColor = Color.white;

        public Dialogue dialogue;

        bool detected = false;

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

        private void OnTriggerStay2D(Collider2D collision)
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (player.isSwinging)
                {
                    detected = false;
                    return;
                }

                if (isActivatable || isRepeatable)
                {
                    if (!detected)
                    {
                        TriggerDialogue();
                        detected = true;
                    }
                }

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CancelDialogue();
                StartCoroutine(CoolDownDialogue());
            }
        }

        IEnumerator CoolDownDialogue()
        {
            yield return new WaitForSeconds(1);
            detected = false;
        }
    }
}