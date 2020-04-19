using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    [SerializeField]
    private int danceCost;
    [SerializeField]
    private int attackCost;
    [SerializeField]
    private float hitTimeout;
    [SerializeField]
    private float flashTime;
    [SerializeField]
    private bool hit;

    private float nextFlashTime;
    private float endHitTime;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (GameManager.instance.IsRunning)
        {
            if (state == ActorState.DANCE | state == ActorState.ATTACK)
            {
                if (Time.time > stateTimeoutTime)
                {
                    state = ActorState.IDLE;
                    frameIndex = 0;
                }
            }

            if (state != ActorState.DANCE & state != ActorState.ATTACK)
            {
                DoInput();
            }

            if (hit)
            {
                if (Time.time > nextFlashTime)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                    nextFlashTime = Time.time + flashTime;
                }
                if (Time.time > endHitTime)
                {
                    hit = false;
                    spriteRenderer.enabled = true;
                }
            }

            if (GameManager.instance.Punks + GameManager.instance.Dorks == 0)
            {
                GameManager.instance.Win();
            }
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

        if (Input.GetButton("Fire2"))
        {
            if (GameManager.instance.DiscoPower > attackCost)
            {
                state = ActorState.ATTACK;
                stateTimeoutTime = Time.time + attackTimeout;
                moveVector = Vector2.zero;
                nextFrameTime = Time.time + (1 / frameTime[(int)ActorState.ATTACK]);
                frameIndex = 0;
                FireAttackProjectiles();
                GameManager.instance.RemovePower(attackCost);
                AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.playerThrowSound);
                return;
            }
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

    protected override IEnumerator FireDanceProjectilesWait()
    {
        yield return new WaitForSeconds(1 / frameTime[(int)ActorState.DANCE]);
        if (GameManager.instance.DiscoPower > danceCost)
        {
            Projectile newProjectile;
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.up);
            newProjectile.SetSourceObject(gameObject);
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.left);
            newProjectile.SetSourceObject(gameObject);
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.right);
            newProjectile.SetSourceObject(gameObject);
            newProjectile = Instantiate(danceProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.SetBaseVector(Vector2.down);
            newProjectile.SetSourceObject(gameObject);
            GameManager.instance.RemovePower(danceCost);
            AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.danceSound);
        }
        else
        {
            state = ActorState.IDLE;
            frameIndex = 0;
            AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.danceFizzleSound);
        }
    }

    public void Hit(int powerCost)
    {
        if (!hit)
        {
            AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.playerHitSound);
            hit = true;
            nextFlashTime = Time.time + flashTime;
            endHitTime = Time.time + hitTimeout;
            spriteRenderer.enabled = false;
            GameManager.instance.RemovePower(powerCost);
        }
    }
}
