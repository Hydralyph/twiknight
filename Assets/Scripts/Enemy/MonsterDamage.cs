using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{


    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            if (collision.collider.gameObject.tag == "Enemies") // destroys anything tagged as Enemies when touching collision box
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1); // looks for collision with game object, checks the componet to see if it has enemy helath, if yes it takes damage
                
            }
       
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
