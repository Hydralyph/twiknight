using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText; // Name Text Field
    public TMP_Text dialogueText; // Dialogue Text Field

    public Animator animator; // For animating the opening and closing of the Dialogue Box

    private Queue<string> sentences; // For the sentences contained in the dialogue sequence

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue) // Core function to start dialogue and tell animator to open the dialogue box with the required text fields filled in
    {
        animator.SetBool("isOpen", true);
        
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence () // To move to the next sequence in the list of the trigger/object
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence) // To 'animate' the letters one by one, akin to common RPG game dialogue boxes
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            float waitFrames = Time.deltaTime * 5;
            yield return new WaitForSeconds(waitFrames);
        }
    }

    public void EndDialogue() // To tell the animator to close the dialogue box once the dialogue has been finished
    {
        Debug.Log("End of conversation");
        animator.SetBool("isOpen", false);
    }

}
