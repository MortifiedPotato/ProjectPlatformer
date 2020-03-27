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

        ParticleSystem dialogueParticle;

        bool detected = false;

        private void Start()
        {
            dialogueParticle = GetComponentInChildren<ParticleSystem>();

            if (isActivatable)
            {
                dialogueParticle.Play();
            }
        }

        public void ManageDialogueTrigger(bool toggle)
        {
            if (toggle || isRepeatable)
            {
                isActivatable = true;
                dialogueParticle.Play();
            }
            else
            {
                isActivatable = false;
                dialogueParticle.Stop();
            }
        }

        void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(this, dialogue, nameColor, dialogueColor);
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

                if (!GameManager.triggeringDialogue)
                {
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
                detected = false;
            }
        }
    }
}