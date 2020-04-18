﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    [SerializeField]
    private int danceCost;
    [SerializeField]
    private int attackCost;

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
            nextFrameTime = Time.time + (1 / frameTime[(int)ActorState.DANCE]);
            FireDanceProjectiles();
            frameIndex = 0;
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

    protected override void FireDanceProjectiles()
    {
        StartCoroutine(FireDanceProjectilesWait());
    }

    IEnumerator FireDanceProjectilesWait()
    {
        yield return new WaitForSeconds(1 / frameTime[(int)ActorState.DANCE]);
        if (GameManager.instance.DiscoPower > danceCost)
        {
            Projectile newProjectile;
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.up);
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.left);
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.right);
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.down);
            GameManager.instance.RemovePower(danceCost);
            // TODO: dance sound
        } else
        {
            state = ActorState.IDLE;
            frameIndex = 0;
            // TODO: dance fizzle sound
        }
    }
}
