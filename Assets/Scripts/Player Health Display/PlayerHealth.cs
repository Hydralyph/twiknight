using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemSO;
using static UnityEngine.Rendering.DebugUI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject YouDiedScreen;
    //keeps track of the players current health
    public int health;
    //how much health you have when you are at full health
    public int maxHealth = 5; // max helath is 5

    public int attack = 1;

    public SpriteRenderer playerSr;
    public PlayerManager playerManager;

    private void Start()
    {
        health = maxHealth;   // start game full health
    }

    public void ChangeHealth(int amountToChangeStat)
     {
        health += amountToChangeStat;
     }
   


    public void TakeDamage(int amount) // function called anytime player takes damage and amount
    {
        health -= amount; // amount passed in brackets above
        if(health <= 0 ) // if the damage takes player to 0 the player is destroyed
        {
            playerSr.enabled = false;
            playerManager.enabled = false;
            YouDiedScreen.SetActive(true);
        }
    }
}
