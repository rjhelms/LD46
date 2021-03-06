﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIActor : Actor
{
    public int AILevel;

    [SerializeField]
    protected GameObject upgradeGameObject;
    [SerializeField]
    protected GameObject downgradeGameObject;

    [SerializeField]
    protected float danceChance;
    [SerializeField]
    protected float attackChance;
    [SerializeField]
    protected float actionFrequency;
    [SerializeField]
    protected float actionFrequencyVariance;

    [SerializeField]
    protected bool canDance;
    [SerializeField]
    protected bool canAttack;
    [SerializeField]
    protected float randomPathDistance;

    [SerializeField]
    protected bool waitingForPath = false;
    [SerializeField]
    protected bool reachedEndOfPath = false;
    [SerializeField]
    protected float nextWaypointDistance = 16.0f;

    [SerializeField]
    protected float searchPathChance;
    [SerializeField]
    protected float searchPathFrequency;

    [SerializeField]
    protected float spawnGraceTime = 0.5f;

    protected int currentWaypoint = 0;
    protected float nextActionTime;
    protected Seeker seeker;
    protected Path path;

    protected bool isChanged = false;

    protected float nextSearchPathTime;
    protected float endSpawnGraceTime;

    protected override void Start()
    {
        base.Start();
        nextActionTime = Time.time + (1 / (actionFrequency * Random.Range(1 - actionFrequencyVariance, 1 + actionFrequencyVariance)));
        seeker = GetComponent<Seeker>();
        seeker.pathCallback += OnPathComplete;
        nextSearchPathTime = Time.time + (1 / searchPathFrequency);
        endSpawnGraceTime = Time.time + spawnGraceTime;
    }

    protected virtual void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }

    protected override void Update()
    {
        if (GameManager.instance.IsRunning)
        {
            if (Time.time > nextSearchPathTime & state == ActorState.IDLE & path == null & waitingForPath == false)
            {
                nextSearchPathTime = Time.time + (1 / searchPathFrequency);
                if (Random.value < searchPathChance)
                {
                    Vector2 targetPosition = (Vector2)transform.position + (Random.insideUnitCircle.normalized * randomPathDistance);
                    seeker.StartPath((Vector2)transform.position, targetPosition);
                }
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
                        }
                        else
                        {
                            reachedEndOfPath = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (!reachedEndOfPath)
                {
                    Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position);
                    direction.Normalize();
                    moveVector = direction * moveSpeed;
                }
                else
                {
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

            if (state == ActorState.DANCE | state == ActorState.ATTACK)
            {
                if (Time.time > stateTimeoutTime)
                {
                    state = ActorState.IDLE;
                    frameIndex = 0;
                }
            }

            if (state != ActorState.DANCE & Time.time > nextActionTime)
            {
                nextActionTime = Time.time + (1 / (actionFrequency * Random.Range(1 - actionFrequencyVariance, 1 + actionFrequencyVariance)));
                float roll = Random.value;
                if (canDance & (roll <= danceChance))
                {
                    DoDance();
                }
                else if (canAttack & (danceChance < roll) & (roll <= attackChance))
                {
                    DoAttack();
                }
            }
        }

        base.Update();
    }

    protected override void DoDance()
    {
        base.DoDance();
        nextActionTime = Time.time + danceTimeout;
    }

    protected override void DoAttack()
    {
        base.DoAttack();
        nextActionTime = Time.time + (1 / (actionFrequency * Random.Range(1 - actionFrequencyVariance, 1 + actionFrequencyVariance)));
        nextActionTime += attackTimeout;
    }

    protected virtual void OnPathComplete(Path p)
    {
        waitingForPath = false;
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public virtual void Upgrade()
    {
        if (upgradeGameObject & Time.time > endSpawnGraceTime)
        {
            if (isChanged)
                return;
            isChanged = true;
            if (spriteRenderer.isVisible)
            {
                AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.upgradeSound);
            }
            Instantiate(upgradeGameObject, transform.position, Quaternion.identity);
            GameManager.instance.AddScore(1000);
            Destroy(gameObject);
        }
    }

    public virtual void Downgrade()
    {
        if (downgradeGameObject & Time.time > endSpawnGraceTime)
        {
            if (isChanged)
                return;
            isChanged = true;
            if (spriteRenderer.isVisible)
            {
                AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.downgradeSound);
            }
            Instantiate(downgradeGameObject, transform.position, Quaternion.identity);
            GameManager.instance.AddScore(-250);
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player " | collision.gameObject.tag == "Enemy" | collision.gameObject.tag == "Friendly")
        {
            if (path != null)
            {
                path = null;
                Debug.Log("Bump, abandoning path");
                // only affect movement & state if walking - otherwise this will get cleared on next timeout
                if (state == ActorState.WALK)
                {
                    state = ActorState.IDLE;
                    moveVector = Vector2.zero;
                }
            }
        }
    }
}
