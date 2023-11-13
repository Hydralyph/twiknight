using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerRescued : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    public void TriggerDialogue ()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
