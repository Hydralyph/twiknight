
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCollision : MonoBehaviour
{

    private bool hasPlayer;

    private void Update()
    {
       if (hasPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Pressed X");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }
    public GameObject testButton;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            testButton.SetActive(true);
            hasPlayer = true;
        }
        
        // Destroy(gameObject, 5);

    }


    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object Exited Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            testButton.SetActive(false);
            hasPlayer = false;
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}
