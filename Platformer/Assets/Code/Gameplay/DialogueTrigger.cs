using UnityEngine;

using SoulHunter.Player;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : Interactable // Mort
    {
        // Toggle trigger dialogue with collision
        public bool canTriggerByCollision;

        // Dialogue Name Text Color
        public Color nameColor = Color.white;

        // Dialogue Sentence Text Color
        public Color dialogueColor = Color.white;

        public Dialogue dialogue;
        ParticleSystem dialogueParticle;
        bool playerDetected = false;

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
                    playerDetected = false;
                    return;
                }

                // If player isn't interacting, return.
                if (!GameManager.interacting && !canTriggerByCollision)
                {
                    return;
                }

                // If either activatable or repeatable, start dialogue.
                if (isActivatable || isRepeatable)
                {
                    if (!playerDetected)
                    {
                        TriggerDialogue();
                        playerDetected = true;
                        canTriggerByCollision = false;
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerDetected = false;
            }
        }
    }
}