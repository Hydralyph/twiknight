
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueColRescue : MonoBehaviour
{
    public DialogueManager dialoguemanager;
    public DialogueTrigger dialoguetrigger;
    // public GameObject testButton; RIP
    public GameObject testButton2; // Name change later
    public TwilightManager twilightmanager;
    private bool hasPlayer;
    private bool hasTalked;
    
    private void Update()
    {
       if (hasPlayer && Input.GetKeyDown(KeyCode.X))
        {
            // PlayerManager.playerManager.CanMove = false;
            Debug.Log("Pressed X");
            dialoguetrigger.TriggerDialogue();
            if (hasTalked == false)
            {
                twilightmanager.twilightrescue += 1;
                Debug.Log("Rescued NPCs: " + twilightmanager.twilightrescue);
                hasTalked = true;
                this.GetComponent<DialogueCollisionSaved>().enabled = true;
                this.GetComponent<DialogueColRescue>().enabled = false;
            }
            else
            {
                Debug.Log("Already talked to, Twilight Rescue: " + twilightmanager.twilightrescue);
                
            }

            // PlayerManager.playerManager.CanMove = true;
        }
    }
    
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            // testButton.SetActive(true);
            testButton2.SetActive(true);
            hasPlayer = true;
        }
        
        // Destroy(gameObject, 5);

    }


    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object Exited Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            // testButton.SetActive(false);
            testButton2.SetActive(false);
            hasPlayer = false;
            dialoguemanager.EndDialogue();
        }
    }
}
