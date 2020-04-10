using UnityEngine;

using SoulHunter.Player;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : Interactable // Mort
    {
        // Dialogue Name Text Color
        public Color nameColor = Color.white;

        // Dialogue Sentence Text Color
        public Color dialogueColor = Color.white;

        public Dialogue dialogue;
        ParticleSystem dialogueParticle;
        bool detected = false;

        private void Start()
        {
            // Get dialogue particle system
            dialogueParticle = GetComponentInChildren<ParticleSystem>();

            // Play particle if dialogue is activatable
            if (isActivatable)
            {
                dialogueParticle.Play();
            }
        }

        /// <summary>
        /// Set Current Dialogue Trigger State
        /// </summary>
        /// <param name="toggle"></param>
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

        /// <summary>
        /// Start dialogue
        /// </summary>
        void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(this, dialogue, nameColor, dialogueColor);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // If player is swinging, return.
                if (PlayerBase.isSwinging)
                {
                    detected = false;
                    return;
                }

                // If player isn't interacting, return.
                if (!GameManager.interacting)
                {
                    return;
                }

                // If either activatable or repeatable, start dialogue.
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