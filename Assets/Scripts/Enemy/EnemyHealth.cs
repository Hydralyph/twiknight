using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 3; // max enemy health
    public int currentHealth;
    public Sprite portrait;
    public AudioClip deathSound;
    public bool IsBoss;
    public GameObject bossManager;
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
            if (IsBoss) bossManager.GetComponent<BossManager>().BossHasDied();

            if (IsBoss) PlayerManager.playerManager.soulPoints += 2;
            else PlayerManager.playerManager.soulPoints += 1;

            Destroy(gameObject);
        }
    }
}
