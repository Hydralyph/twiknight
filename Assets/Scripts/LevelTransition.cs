using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string sceneName;

    //private void Start()
    //{
    //    inputManager.InteractEvent += OnPlayerInteract;
    //}

    //private void OnPlayerInteract()
    //{
    //    // IMPLEMENT SOME FORM OF CHECKING
    //    throw new NotImplementedException();
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            if(Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }


}
