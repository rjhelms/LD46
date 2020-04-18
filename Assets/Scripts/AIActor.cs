using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActor : Actor
{
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

    protected float nextActionTime;

    protected override void Start()
    {
        base.Start();
        nextActionTime = Time.time + (1 / (actionFrequency * Random.Range(1 - actionFrequencyVariance, 1 + actionFrequencyVariance)));
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
        if (state != ActorState.DANCE & Time.time > nextActionTime)
        {
            nextActionTime = Time.time + (1 / (actionFrequency * Random.Range(1 - actionFrequencyVariance, 1 + actionFrequencyVariance)));
            float roll = Random.value;
            if (canDance & (roll <= danceChance))
            {
                DoDance();
            } else if (canAttack & (danceChance < roll) & (roll <= attackChance))
            {
                DoAttack();
            }
        }

        base.Update();
    }

    protected override void DoDance()
    {
        base.DoDance();
        nextActionTime = Time.time + danceTimeout;
        Debug.Log("dancing");
    }

    protected override void DoAttack()
    {
        base.DoAttack();
        Debug.Log("attacking");
    }
}
