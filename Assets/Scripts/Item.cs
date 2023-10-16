using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//kallum best 2023 270116003
public class Item : MonoBehaviour
{
    //dont want other scripts editing this and causeing bugs
    [SerializeField]
    private string itemName;
    //amount of items
    [SerializeField]
    private int quantity;
    //keeping track of images of sprites
    [SerializeField]
    private Sprite sprite;

    [TextArea] // adds item description for UI component for items
    [SerializeField]
    private string itemDescription;

    //script to talk to inventory
    private InventoryManager inventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        // telling script how to find it
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }
    // anytime this item collides with something this activates
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checking to see if collision game object was in fact the player. 
        if (collision.gameObject.tag == "Player")
        {
            //talks to inventory manager. add item with qualnity name and sprite and destroys that game object
            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            Destroy(gameObject);

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
