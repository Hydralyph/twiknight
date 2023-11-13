using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TwilightSwitch : MonoBehaviour
{
    // public GameObject testButton; RIP
    public DialogueManager dialoguemanager;
    public DialogueTrigger dialoguetrigger;
    public GameObject testButton2; // Name change later
    public TwilightManager twilightmanager;
    private bool hasPlayer;

    private void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.X) && twilightmanager.isTwilightActive)
        {
            // PlayerManager.playerManager.CanMove = false;
            Debug.Log("Pressed X");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            // PlayerManager.playerManager.CanMove = true;
        }
        else if (hasPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Pressed X");
            dialoguetrigger.TriggerDialogue();
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