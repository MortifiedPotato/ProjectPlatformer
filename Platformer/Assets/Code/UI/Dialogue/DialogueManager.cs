using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace SoulHunter.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        private Queue<string> sentences;

        bool nonTriggerable;

        Animator animator;

        [SerializeField] TMP_FontAsset dialogueFont;
        [SerializeField] TMP_FontAsset normalFont;

        [SerializeField] GameObject dialogueBox;

        [SerializeField] Button ContinueButton;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI dialogueText;

        void Start()
        {
            Instance = this;
            sentences = new Queue<string>();

            animator = dialogueBox.GetComponent<Animator>();
        }

        public void StartDialogue(Dialogue _dialogue, Color _nameColor, Color _dialogueColor)
        {
            animator.SetBool("inDialogue", true);
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

        IEnumerator TypeSentence(string _sentence)
        {
            dialogueText.text = "";
            foreach (char letter in _sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(">>End of conversation.<<"));
            nonTriggerable = true;
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