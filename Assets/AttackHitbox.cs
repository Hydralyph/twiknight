using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (collision.collider.gameObject.tag == "Enemies") collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        if (col.gameObject.tag == "Enemies") col.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
    }
}
