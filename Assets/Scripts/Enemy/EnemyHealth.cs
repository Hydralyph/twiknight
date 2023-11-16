using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 3; // max enemy health
    public int currentHealth;
    public Sprite portrait;
    public AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount) // takes damage amount and passses it
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            Destroy (gameObject);   
        }
    }
}
