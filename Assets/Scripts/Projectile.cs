﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifespan;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 baseMoveVector;
    [SerializeField]
    private Vector2 currentMoveVector;
    [SerializeField]
    private float moveRandomness;
    [SerializeField]
    private float moveRandomInterval;

    [SerializeField]
    private bool spin;
    [SerializeField]
    private float spinFrequency;



    [SerializeField]
    private int upgradeLevel = -1;      // -1 means nothing
    [SerializeField]
    private int downgradeLevel = -1;    // -1 means nothing

    private float nextRandomInterval;
    private float lifespanEnd;
    private float nextSpinTime;
    private new Rigidbody2D rigidbody2D;
    private GameObject sourceObject;

    public void SetBaseVector(Vector2 newVector)
    {
        baseMoveVector = newVector.normalized;
    }

    public void SetSourceObject(GameObject source)
    {
        sourceObject = source;
    }


    // Start is called before the first frame update
    void Start()
    {
        lifespanEnd = Time.time + lifespan;
        if (moveRandomness > 0)
        {
            nextRandomInterval = Time.time + moveRandomInterval;
        }
        rigidbody2D = GetComponent<Rigidbody2D>();
        baseMoveVector.Normalize();
        currentMoveVector = baseMoveVector;
        if (spin)
        {
            nextSpinTime = Time.time + (1 / spinFrequency);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lifespanEnd)
        {
            Destroy(gameObject);
        }
        if (moveRandomness > 0 & Time.time > nextRandomInterval)
        {
            nextRandomInterval = Time.time + moveRandomInterval;
            Vector2 randomVector = new Vector2(Random.Range(-moveRandomness, moveRandomness), Random.Range(-moveRandomness, moveRandomness));
            currentMoveVector = baseMoveVector + randomVector;
            currentMoveVector.Normalize();
        }
        if (spin & Time.time > nextSpinTime)
        {
            nextSpinTime = Time.time + (1 / spinFrequency);
            transform.Rotate(0, 0, 90);
        }
        rigidbody2D.velocity = currentMoveVector * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == sourceObject)
        {
            return;
        }

        AIActor actor = collision.GetComponent<AIActor>();
        if (actor)
        {
            if (actor.AILevel == upgradeLevel)
            {
                actor.Upgrade();
                Destroy(gameObject);
            } else if (actor.AILevel == downgradeLevel)
            {
                actor.Downgrade();
                Destroy(gameObject);
            }
        }
    }
}
