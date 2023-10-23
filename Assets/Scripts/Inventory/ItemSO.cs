using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemSO;


// add create menu allows you to create instances of this sciptable object by clicking Create in Unity
[CreateAssetMenu]
public class ItemSO : ScriptableObject

{
    public string itemName;
    // player can access variables
    public StatToChange statToChange = new StatToChange();
    // amount to change stat
    public int amountToChangeStat;
    //enumeration allows you to create drop down menus of related constants


    public AttributeToChange attributeToChange = new AttributeToChange();
    // amount to change stat
    public int amountToChangeAttribute;

    public int Heart;


    // how to use item

   
    public void UseItem()
   {
       if(statToChange == StatToChange.health)
       {
           GameObject.Find("Heart").GetComponent<PlayerHealth>().ChangeHealth(amountToChangeStat);
            
        }
   }



    public enum StatToChange
    {
        none,
        health,
        SoulPoint,
        attack
    }; // semicolon to make work

    public enum AttributeToChange
    {
        none,
        strength,
        defense
    }; // semicolon to make work

}
