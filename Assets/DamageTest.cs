using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    private BoxCollider2D enemyCollider;
    [SerializeField] private LayerMask Damageable;

    private void Start()
    {
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Physics2D.OverlapBox(new Vector2(enemyCollider.bounds.min.x + (enemyCollider.bounds.min.x / 2), enemyCollider.bounds.center.y), new Vector2(2f, 2f), 0f, Damageable);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(new Vector3(enemyCollider.bounds.min.x, enemyCollider.bounds.center.y, 0f), new Vector3(2f, 2f, 0f));
    }
}
