using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelCollider : MonoBehaviour
{
    [SerializeField] GameObject warningBox;
    [SerializeField] TwilightTimer twiTimer;
    private bool hasPlayer;

    void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.X))
        {
            warningBox.SetActive(false);
            twiTimer.remainingTime = -1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered Dialogue Trigger");
        if (other.CompareTag("Player"))
        {
            // testButton.SetActive(true);
            warningBox.SetActive(true);
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
            warningBox.SetActive(false);
            hasPlayer = false;
        }
    }

}
