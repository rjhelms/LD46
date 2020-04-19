using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak : AIActor
{
    protected override void Update()
    {
        if (state == ActorState.IDLE & path == null & waitingForPath == false)
        {
            Vector2 targetPosition = (Vector2)transform.position + (Random.insideUnitCircle.normalized * randomPathDistance);
            seeker.StartPath((Vector2)transform.position, targetPosition);
        }
        if (path != null & (state == ActorState.IDLE | state == ActorState.WALK))
        {
            float distanceToWaypoint;
            reachedEndOfPath = false;
            while (true)
            {
                distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distanceToWaypoint < nextWaypointDistance)
                {
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    } else
                    {
                        reachedEndOfPath = true;
                        break;
                    }
                } else
                {
                    break;
                }
            }

            if (!reachedEndOfPath)
            {
                Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position);
                direction.Normalize();
                moveVector = direction * moveSpeed;
            } else
            {
                Debug.Log("Reached end of path!");
                path = null;
                moveVector = Vector2.zero;
            }

            if (moveVector.magnitude == 0)
            {
                state = ActorState.IDLE;
            }
            else
            {
                state = ActorState.WALK;
            }
        }
        base.Update();
    }
}
