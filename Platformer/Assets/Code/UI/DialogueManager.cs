using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using SoulHunter.Player;
using UnityEngine.EventSystems;

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
        public DialogueTrigger currentTrigger;

        bool nonTriggerable;

        Animator animator;

        [Header("Fonts")] // Font variables
        [SerializeField] TMP_FontAsset dialogueFont;
        [SerializeField] TMP_FontAsset normalFont;

        [Header("Objects")] // Object variables
        [SerializeField] GameObject dialogueBox;
        [SerializeField] Button ContinueButton;

        [Header("Images")] // Image variables
        [SerializeField] Image portraitImage;

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
        public void StartDialogue(DialogueTrigger trigger, Dialogue _dialogue, Sprite _portrait, Color _nameColor, Color _dialogueColor)
        {
            currentTrigger = trigger;

            PlayerBase.isPaused = true;

            animator.SetBool("inDialogue", true);
            GameManager.initiatedDialogue = true;
            ContinueButton.Select();

            nameText.text = _dialogue.name;
            nameText.color = _nameColor;
            dialogueText.color = _dialogueColor;

            if (_portrait)
            {
                portraitImage.color = Color.white;
                portraitImage.sprite = _portrait;
                portraitImage.preserveAspect = true;
            }
            else
            {
                portraitImage.color = Color.clear;
            }

            sentences.Clear();

            foreach (string sentence in _dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();

            AudioManager.PlaySound(AudioManager.Sound.StartDialogue, currentTrigger.transform.position);
        }

        /// <summary>
        /// Types the next sentence in the dialogue box
        /// </summary>
        public void DisplayNextSentence()
        {
            ContinueButton.Select();

            if (nonTriggerable)
            {
                animator.SetBool("inDialogue", false);
                GameManager.initiatedDialogue = false;
                PlayerBase.isPaused = false;
                nonTriggerable = false;
            }
            else
            {
                if (sentences.Count == 0)
                {
                    dialogueText.alignment = TextAlignmentOptions.Center;
                    dialogueText.fontStyle = FontStyles.Italic;
                    dialogueText.color = Color.white;
                    dialogueText.font = normalFont;
                    dialogueText.fontSize = 20;
                    EndDialogue();
                    return;
                }

                dialogueText.alignment = TextAlignmentOptions.TopLeft;
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
        void EndDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(">>End of conversation.<<"));
            GameManager.initiatedDialogue = false;
            PlayerBase.isPaused = false;
            nonTriggerable = true;

            animator.SetBool("inDialogue", false);

            currentTrigger.ManageDialogueTrigger(false);

            EventSystem.current.SetSelectedGameObject(null);

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
    }

    [System.Serializable]
    public class Dialogue
    {
        public string name;
        [TextArea(3, 10)]
        public string[] sentences;
    }
}