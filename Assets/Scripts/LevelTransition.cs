using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] Scene destinationScene;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            
        }
    }
}
