using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    // reference to object we want to turn on and off
    public GameObject InventoryMenu;
    //private bool to keep track if the menu is on or off
    private bool menuActivated;

    //Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if statement to check for input button i
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            // Deactivates Menu
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

        // if statement to check for input button i, adding else only executes if previous statement is NOT true
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            // Activates menu 
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
}
