
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCollision : MonoBehaviour
{
    public DialogueManager dialoguemanager;
    public DialogueTrigger dialoguetrigger;
    public GameObject testButton; 
    public GameObject testButton2;
    private bool hasPlayer;
    
    private void Update()
    {
       if (hasPlayer && Input.GetKeyDown(KeyCode.X)) // Reads player input only if the player exists inside the collision field
        {
            // PlayerManager.playerManager.CanMove = false; (Redundant)
            Debug.Log("Pressed X");
            dialoguetrigger.TriggerDialogue();
            // PlayerManager.playerManager.CanMove = true; (Redundant)
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) // tells the game the player is inside the collision field, and brings up the on screen prompt for interaction
    {
        Debug.Log("Object Entered Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            testButton.SetActive(true);
            testButton2.SetActive(true);
            hasPlayer = true;
        }
        
        // Destroy(gameObject, 5); For testing purposes only

    }


    void OnTriggerExit2D(Collider2D other) // To close Dialogue Box if the player abandons the collision field during dialogue
    {
        Debug.Log("Object Exited Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            testButton.SetActive(false);
            testButton2.SetActive(false);
            hasPlayer = false;
            dialoguemanager.EndDialogue();
        }
    }
}
