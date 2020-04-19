using UnityEngine;
using SoulHunter.Player;

namespace SoulHunter.Dialogue
{
    public class DialogueTrigger : Interactable // Mort
    {
        // Toggle trigger dialogue with collision
        public bool canTriggerByCollision;

        // bool whether this dialogue is a tutorial
        [SerializeField] bool isTutorial;

        // The NPC this dialogue belongs to
        public GameObject NPC;

        // Dialogue instance
        public Dialogue dialogue;

        // Dialogue particle
        ParticleSystem dialogueParticle;

        Collider2D[] dialogueBorders; // Workaround because I don't want to touch the enemies' code - That's Thomas' area

        private void Start()
        {
            // If dialogue contains no exchanges, deactivate.
            if (dialogue.exchanges.Length < 1)
            {
                isActivatable = false;
                return;
            }
            else
            {
                // If dialogue is tutorial and tutorials are disabled, deactivate.
                if (isTutorial && !GameSettings.tutorialsEnabled)
                {
                    isActivatable = false;
                    return;
                }
            }

            // Get dialogue particle system
            dialogueParticle = GetComponentInChildren<ParticleSystem>();

            // Get dialogue border colliders
            dialogueBorders = GetComponentsInChildren<Collider2D>();

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

                for (int i = 0; i < dialogueBorders.Length; i++)
                {
                    dialogueBorders[i].enabled = false;
                }
            }
        }

        /// <summary>
        /// Starts dialogue
        /// </summary>
        void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(this, dialogue);

            for (int i = 0; i < dialogueBorders.Length; i++)
            {
                dialogueBorders[i].enabled = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            // If dialogue is already initiated, return.
            if (GameManager.initiatedDialogue) return;

            // If collision is on the player layer
            if (collision.transform.gameObject.layer == 10)
            {
                // If player isn't interacting and dialogue can't be triggered by collision, return.
                if (!GameManager.interacting && !canTriggerByCollision) return;

                // If player is swinging, return.
                if (PlayerBase.isSwinging) return;

                // If either activatable or repeatable, start dialogue.
                if (isActivatable || isRepeatable)
                {
                    TriggerDialogue();

                    if (GameManager.initiatedDialogue)
                    {
                        canTriggerByCollision = false;
                    }
                }
            }
        }
    }
}