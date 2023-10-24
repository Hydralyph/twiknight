using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{

    // reference to object we want to turn on and off
    public GameObject SkillTreeMenu;
    //private bool to keep track if the menu is on or off
    private bool menuActivated;


    void Update()
    {
        // if statement to check for input button i
        if (Input.GetButtonDown("SkillTree") && menuActivated)
        {
            //paused time deactived
            Time.timeScale = 1;
            // Deactivates Menu
            SkillTreeMenu.SetActive(false);
            menuActivated = false;
        }// Start is called before the first frame update

        // if statement to check for input button i, adding else only executes if previous statement is NOT true
        else if (Input.GetButtonDown("SkillTree") && !menuActivated)
        {
            // Activates menu 
            //pauses game time and physics
            Time.timeScale = 0;
            SkillTreeMenu.SetActive(true);
            menuActivated = true;
        }
    }


    void Start()
        {

        }


  }

