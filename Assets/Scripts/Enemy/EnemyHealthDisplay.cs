using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adding ui library
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{


    public int health; // monsters current health
    public int maxHealth; // health when full

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts; // allows to drag and drop as many objects as you want within Unity
                           // Start is called before the first frame update

    public Image portraitImage; // store enemys face image
    public Sprite portrait;

    public EnemyHealth enemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        FindClosestEnemy();
    }


    public void FindClosestEnemy()
    {
        float closestEnemyDistance = Mathf.Infinity; // searches entire scene for enemies
        EnemyHealth nearestEnemy = null;

  
        EnemyHealth[] potentialTargets = FindObjectsOfType<EnemyHealth>();
        
        foreach(EnemyHealth currentEnemy in potentialTargets)
        {
            float distanceAway = (currentEnemy.transform.position - transform.position).sqrMagnitude; // figuring out distance from our current ememy to our player
            if (distanceAway < closestEnemyDistance)
            {
                closestEnemyDistance = distanceAway; // if object is closest it will display enemy health
                nearestEnemy = currentEnemy;

                maxHealth = nearestEnemy.maxHealth;
                health = nearestEnemy.currentHealth;
                portraitImage.sprite = nearestEnemy.portrait; // dont really need this part is just for portrait
            }
        
        }

        if(closestEnemyDistance <= 50) // if enemy distance is less that 25 turn health bar icons on
        {
            HealthBarOn();
        }
        else
        {
            HealthBarOff();
        }
    }

    public void HealthBarOff() // turns enemy health bar off
    {
        portraitImage.enabled = false;
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = false;
        }
    }




    public void HealthBarOn()
    {

        portraitImage.enabled = true;

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
