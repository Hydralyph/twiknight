using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private float friction;

    public bool GetOnGround()
    {
        return onGround;
    }

    public float GetFriction()
    {
        return friction;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        EvaluateCollision(col);
        RetrieveFriction(col);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        EvaluateCollision(col);
        RetrieveFriction(col);
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        onGround = false;
        friction = 0.0f;
    }

    private void EvaluateCollision(Collision2D col)
    {
        for(int i = 0; i < col.contactCount; i++)
        {
            Vector2 normal = col.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }

    private void RetrieveFriction(Collision2D col)
    {
        PhysicsMaterial2D mat = col.rigidbody.sharedMaterial;

        friction = 0;

        if(mat != null)
        {
            friction = mat.friction;
        }
    }
}
