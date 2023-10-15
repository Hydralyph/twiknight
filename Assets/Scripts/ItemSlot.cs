using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// text mesh pro using line
using TMPro;
// Ui interaction
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour

{

    // ========ITEM DATA=======
    //public for debuging purposes can be set as private later on.
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;


    //=========ITEM SLOT========
    //will display image, quaunity etc
    [SerializeField]
    // serileze field makes this variable visible and editable in the unity inspector
    private TMP_Text quantityText; // this will display quantity text

    [SerializeField] // will show image in slot
    private Image itemImage;

    // custom method saying its going to be recieving information about item name quantity and item sprite info
    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        //just saying that its going to be the same as x = x each other
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        //cusotmise the look telling it that a textmesh pro text compopnent is = to a number
        quantityText.text = quantity.ToString();
        //ToString turns it into interger
        quantityText.enabled = true;
        //making sure image is = to sprite image that was sent in
        itemImage.sprite = itemSprite;
}

}
