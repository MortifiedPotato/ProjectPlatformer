using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using SoulHunter.Player;

namespace SoulHunter.Dialogue
{
    public class DialogueManager : MonoBehaviour // Mort
    {
        // Singleton Instance
        public static DialogueManager Instance;

        // Queue of dialogue sentences
        public Queue<string> sentences;

        // Currently triggered dialogue
        public DialogueTrigger currentTrigger;

        bool nonTriggerable;

        Animator animator;

        [Header("Fonts")]
        [SerializeField] TMP_FontAsset dialogueFont;
        [SerializeField] TMP_FontAsset normalFont;

        [Header("Objects")]
        [SerializeField] GameObject dialogueBox;
        [SerializeField] Button ContinueButton;

        [Header("Texts")]
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI dialogueText;

        void Start()
        {
            Instance = this;
            sentences = new Queue<string>();

            animator = dialogueBox.GetComponent<Animator>();
        }

        public void StartDialogue(DialogueTrigger trigger, Dialogue _dialogue, Color _nameColor, Color _dialogueColor)
        {
            currentTrigger = trigger;

            PlayerBase.isPaused = true;

            animator.SetBool("inDialogue", true);
            GameManager.initiatedDialogue = true;
            ContinueButton.Select();

            nameText.text = _dialogue.name;
            nameText.color = _nameColor;
            dialogueText.color = _dialogueColor;

            sentences.Clear();

            foreach (string sentence in _dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

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

        void EndDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(">>End of conversation.<<"));
            GameManager.initiatedDialogue = false;
            PlayerBase.isPaused = false;
            nonTriggerable = true;

            animator.SetBool("inDialogue", false);

            currentTrigger.ManageDialogueTrigger(false);
        }

        IEnumerator TypeSentence(string _sentence)
        {
            dialogueText.text = "";
            foreach (char letter in _sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(GameSettings.dialogueSpeed / 10);
            }
        }

        public void PauseCheck()
        {
            if (GameManager.GameIsPaused)
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