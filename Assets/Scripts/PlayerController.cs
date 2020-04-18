using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public class PlayerController : Actor
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        if (inputVector.magnitude > 1)
            inputVector.Normalize();
        moveVector = inputVector * moveSpeed;
        base.Update();
    }

}
