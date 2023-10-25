using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// line for using UI
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{


    public int health; // players current health
    public int maxHealth; // health when full

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts; // allows to drag and drop as many objects as you want within Unity
                           // Start is called before the first frame update


    public PlayerHealth playerHealth;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;
        for (int i = 0; i < hearts.Length; i++) // this lets the enemy damage script know where to find the player Health script
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
