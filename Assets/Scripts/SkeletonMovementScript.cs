using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovementScript : MonoBehaviour
{

    public Transform[] patrolPoints; // adding array for location of patrol points monster will move to and back from
    public float moveSpeed; // speed of skeleton movement
    public int patrolDestination;
    
  

    // Update is called once per frame
    void Update()
    {
        // unity chekcing patrol point locations and sending them monster to it.
        if (patrolDestination == 0)
        {                                              /// from this position   to new position and speed
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
        //check to tsee if skeleton has moved to destination
        if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                transform.localScale = new Vector3(1, 1, 1); // effects scale turns around enemy
                //send enemy to new destination
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {                                              /// from this position   to new position and speed
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            //check to tsee if skeleton has moved to destination
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                transform.localScale = new Vector3(-1, 1, 1); // effects scale turns around enemy
                //send enemy to new destination
                patrolDestination = 0;
            }
        }

    }
}
