using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using SoulHunter.Player;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace SoulHunter.Dialogue
{
    public class DialogueManager : MonoBehaviour // Mort
    {
        // Singleton Instance
        public static DialogueManager Instance;

        // Queue of dialogue sentences
        public Queue<string> sentences;

        // Currently triggered dialogue
        [HideInInspector]
        public Dialogue currentDialogue;
        public DialogueTrigger currentTrigger;

        bool nonTriggerable;
        int currentExchange;

        Animator animator;

        [Header("Fonts")] // Font variables
        [SerializeField] TMP_FontAsset dialogueFont;
        [SerializeField] TMP_FontAsset normalFont;

        [Header("Objects")] // Object variables
        [SerializeField] GameObject dialogueBox;
        [SerializeField] Button ContinueButton;

        [Header("Images")] // Image variables
        [SerializeField] Image portraitImage;
        [SerializeField] Image portraitFrame;

        [Header("Texts")] // Text variables
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI dialogueText;

        void Start()
        {
            Instance = this;
            sentences = new Queue<string>();

            animator = dialogueBox.GetComponent<Animator>();
        }

        /// <summary>
        /// Initializes dialogue and sets respective text colors
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="_dialogue"></param>
        /// <param name="_nameColor"></param>
        /// <param name="_dialogueColor"></param>
        public void StartDialogue(DialogueTrigger trigger, Dialogue _dialogue)
        {
            currentTrigger = trigger;
            currentDialogue = _dialogue;

            currentExchange = 0;

            GameManager.initiatedDialogue = true;
            PlayerBase.isPaused = true;

            animator.SetBool("inDialogue", GameManager.initiatedDialogue);
            ContinueButton.Select();

            sentences.Clear();

            CheckNextSentence(_dialogue.exchanges[currentExchange]);

            AudioManager.PlaySound(AudioManager.Sound.StartDialogue, currentTrigger.transform.position);
        }

        public void CheckNextSentence(DialogueExchange exchange)
        {
            UpdateDialogueElements();

            foreach (string sentence in exchange.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        /// <summary>
        /// Types the next sentence in the dialogue box
        /// </summary>
        public void DisplayNextSentence()
        {
            ContinueButton.Select();

            HandleNPCAnimation();

            if (nonTriggerable)
            {
                GameManager.initiatedDialogue = false;
                animator.SetBool("inDialogue", GameManager.initiatedDialogue);
                PlayerBase.isPaused = false;
                nonTriggerable = false;
            }
            else
            {
                if (sentences.Count == 0)
                {
                    if (currentDialogue.exchanges.Length > currentExchange + 1)
                    {
                        currentExchange++;
                        CheckNextSentence(currentDialogue.exchanges[currentExchange]);
                        return;
                    }
                    dialogueText.fontStyle = FontStyles.Italic;
                    dialogueText.color = Color.white;
                    dialogueText.font = normalFont;
                    dialogueText.fontSize = 20;
                    dialogueText.text = "";
                    currentDialogue = null;
                    EndDialogue();
                    return;
                }

                dialogueText.fontStyle = FontStyles.Normal;
                dialogueText.font = dialogueFont;
                dialogueText.fontSize = 36;
                string sentence = sentences.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }
        }

        /// <summary>
        /// Indicates end of dialogue and handles the termination of current dialogue
        /// </summary>
        public void EndDialogue()
        {
            StopAllCoroutines();
            UpdateDialogueElements();
            GameManager.initiatedDialogue = false;
            PlayerBase.isPaused = false;
            nonTriggerable = true;

            animator.SetBool("inDialogue", GameManager.initiatedDialogue);

            currentTrigger.ManageDialogueTrigger(false);

            EventSystem.current.SetSelectedGameObject(null);

            if (currentTrigger.NPC)
            {
                Destroy(currentTrigger.NPC, 1);
            }

            AudioManager.PlaySound(AudioManager.Sound.EndDialogue, currentTrigger.transform.position);
        }

        /// <summary>
        /// Types dialogue in a typewriter style
        /// </summary>
        /// <param name="_sentence"></param>
        /// <returns></returns>
        IEnumerator TypeSentence(string _sentence)
        {
            dialogueText.text = "";
            foreach (char letter in _sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(GameSettings.dialogueSpeed / 10);
            }
        }

        void UpdateDialogueElements()
        {
            CharacterData person = currentDialogue?.exchanges[currentExchange].character;

            if (person)
            {
                nameText.text = person.name;
                nameText.color = person.nameColor;
                dialogueText.color = person.dialogueColor;

                if (person.portrait)
                {
                    portraitImage.color = Color.white;
                    portraitFrame.color = Color.white;
                    portraitImage.sprite = person.portrait;
                    portraitImage.preserveAspect = true;
                }
                else
                {
                    portraitImage.color = Color.clear;
                    portraitFrame.color = Color.clear;
                }
            }
            else
            {
                nameText.text = "";
                nameText.color = Color.white;
                dialogueText.color = Color.white;

                portraitImage.color = Color.clear;
                portraitFrame.color = Color.clear;
            }
        }

        void HandleNPCAnimation()
        {
            if (!currentTrigger.NPC) return;

            if (currentDialogue.exchanges[currentExchange].isPointing)
            {
                currentTrigger.NPC?.GetComponentInChildren<Animator>().SetBool("isPointing", true);
            }
            else
            {
                currentTrigger.NPC?.GetComponentInChildren<Animator>().SetBool("isPointing", false);
            }
        }

        /// <summary>
        /// Pauses dialogue when game is paused
        /// </summary>
        public void PauseCheck()
        {
            if (GameManager.gameIsPaused)
            {
                animator.SetBool("GameIsPaused", true);
            }
            else
            {
                animator.SetBool("GameIsPaused", false);
            }
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GameManager.initiatedDialogue)
                {
                    ContinueButton.Select();
                }
            }
        }

        private void OnDestroy()
        {
            GameManager.initiatedDialogue = false;
        }
    }

    [System.Serializable]
    public class Dialogue
    {
        public DialogueExchange[] exchanges;
    }

    [System.Serializable]
    public class DialogueExchange
    {
        public bool isPointing;

        public CharacterData character;

        [TextArea(3, 10)]
        public string[] sentences;
    }
}