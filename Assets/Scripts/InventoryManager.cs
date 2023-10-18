using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//kallum best 2023 270116003
public class InventoryManager : MonoBehaviour
{

    // reference to object we want to turn on and off
    public GameObject InventoryMenu;
    //private bool to keep track if the menu is on or off
    private bool menuActivated;
    //for item slots can be adjusted between the array [] has array for all slots
    public ItemSlot[] itemSlot;
    

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
            //paused time deactived
            Time.timeScale = 1;
            // Deactivates Menu
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

        // if statement to check for input button i, adding else only executes if previous statement is NOT true
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            // Activates menu 
            //pauses game time and physics
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    //creating new method for Add item and telling ti what items in brackets is coming into it
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //to test it to make sure its working adding debug here
        // Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);

        //slots adding number of items in array it will continue to loop through
        for (int i = 0; i < itemSlot.Length; i++)
        {
            //checking for each slot if that item slot if it NOT full, if not will tell it add that info
         if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
        return; 
            }
        }
        
    }

    // command to deselect all slots selected in inventory
    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            // each item slot it comes across will set as unactive
            itemSlot[i].selectedShader.SetActive(false);
            //letting it know the slots are no longer active
            itemSlot[i].thisItemSelected = false;
        }
    }
}
