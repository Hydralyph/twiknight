using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// text mesh pro using line
using TMPro;
// Ui interaction
using UnityEngine.UI;
using UnityEngine.EventSystems;

// adding feature that when object is clicked it knows its being clicked
public class ItemSlot : MonoBehaviour, IPointerClickHandler

{

    // ========ITEM DATA======= // IFORMATION OF THE ITEMS
    //public for debuging purposes can be set as private later on.
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    //mkaes item slot empty
    public Sprite emptySprite;

    //=========ITEM SLOT======== // WHAT THE SLOT WILL DISPLAY
    //will display image, quaunity etc
    [SerializeField]
    // serileze field makes this variable visible and editable in the unity inspector
    private TMP_Text quantityText; // this will display quantity text

    [SerializeField] // will show image in slot
    private Image itemImage;

    //=========ITEM DESCRIPTION SLOT======== // WHAT WILL SHOW IN DESCRIPTION PANNEL
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;



    //reference to game data for shader we want to turn on and that the item has been selected
    public GameObject selectedShader;
    public bool thisItemSelected;

    //call to turn all slots off this script talks to inventory manager script
    private InventoryManager inventoryManager;
    // talks to inventory manager script
    private void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }

    // custom method saying its going to be recieving information about item name quantity and item sprite info
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //just saying that its going to be the same as x = x each other
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        //cusotmise the look telling it that a textmesh pro text compopnent is = to a number
        quantityText.text = quantity.ToString();
        //ToString turns it into interger
        quantityText.enabled = true;
        //making sure image is = to sprite image that was sent in
        itemImage.sprite = itemSprite;
}

    // when item is clicked it will call what is in On Pointer Click
    public void OnPointerClick(PointerEventData eventData)
    {
        //allows us to detect when the left mouse button has been clicked
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //if it is LEFT it will do this
            OnLeftClick();
           
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //if it is RIGHT it will do this
            OnRightClick();
        }
    }

    //what happens on a new click
    public void OnLeftClick()
    {

        if (thisItemSelected) // if left click is double clicked items effect is applied
            inventoryManager.UseItem(itemName);

        inventoryManager.DeselectAllSlots(); // when left click on something tells it to deslect all other slots and turn this on
        //call to turn all slots off
        selectedShader.SetActive(true); // turns on shader
        thisItemSelected = true;
        //change what data appears
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        // makes sprite box empty if not selecting sprite
        if (itemDescriptionImage.sprite == null)
            itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
        
    }
}
