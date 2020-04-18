using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (state == ActorState.DANCE)
        {
            if (Time.time > stateTimeoutTime)
            {
                state = ActorState.IDLE;
                frameIndex = 0;
            }
        }

        if (state != ActorState.DANCE)
        {
            DoInput();
        }

        base.Update();
    }
    
    void DoInput()
    {
        if (Input.GetButton("Fire1"))
        {
            state = ActorState.DANCE;
            stateTimeoutTime = Time.time + danceTimeout;
            moveVector = Vector2.zero;
            nextFrameTime = Time.time + frameTime[(int)ActorState.DANCE];
            FireDanceProjectiles();
            return;
        }

        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        if (inputVector.magnitude > 1)
            inputVector.Normalize();
        moveVector = inputVector * moveSpeed;
        if (moveVector.magnitude == 0)
        {
            state = ActorState.IDLE;
        } else
        {
            state = ActorState.WALK;
        }
    }
}
