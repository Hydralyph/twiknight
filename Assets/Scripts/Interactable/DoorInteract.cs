using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour
{
    public GameObject promptBtn;
    public string sceneName;
    private bool hasPlayer;

    void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.W)) {
            Debug.Log("Pressed UP Arrow");
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered Door Trigger");
        if (other.CompareTag("Player"))
        {
            // testButton.SetActive(true);
            promptBtn.SetActive(true);
            hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object Exited Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            // testButton.SetActive(false);
            promptBtn.SetActive(false);
            hasPlayer = false;
        }
    }

}
