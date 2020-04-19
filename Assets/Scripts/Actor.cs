using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST,
}

public enum ActorState
{
    IDLE = 0,
    WALK = 1,
    DANCE = 2,
    ATTACK = 3,
}

public class Actor : MonoBehaviour
{
    [SerializeField]
    protected ActorState state;

    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected Vector2 moveVector;
    [SerializeField]
    protected float[] frameTime;
    [SerializeField]
    protected Direction direction = Direction.SOUTH;

    [SerializeField]
    protected int frameIndex = 0;
    [SerializeField]
    protected Sprite[] walkSprites; // create in order NESW
    [SerializeField]
    protected Sprite[] danceSprites;
    [SerializeField]
    protected float danceTimeout = 1.0f;
    [SerializeField]
    protected Sprite[] attackSprites;
    [SerializeField]
    protected float attackTimeout = 0.5f;

    [SerializeField]
    protected GameObject danceProjectilePrefab;
    [SerializeField]
    protected GameObject attackProjectilePrefab;
    [SerializeField]
    protected Transform projectileSpawnPoint;

    protected int walkFrames;
    protected float nextFrameTime;
    protected float stateTimeoutTime;

    protected SpriteRenderer spriteRenderer;
    protected new Rigidbody2D rigidbody2D;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        walkFrames = walkSprites.Length / 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        nextFrameTime = Time.time + (1 / frameTime[(int)ActorState.WALK]);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SetDirection();
        SetSprite();
    }

    protected void SetDirection()
    { 
        if (moveVector.magnitude == 0)
        {
            return;
        }
        if (Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
        {
            if (moveVector.x < 0)
            {
                direction = Direction.WEST;
            } else
            {
                direction = Direction.EAST;
            }
        } else
        {
            if (moveVector.y < 0 )
            {
                direction = Direction.SOUTH;
            } else
            {
                direction = Direction.NORTH;
            }
        }

    }
    protected void SetSprite()
    {
        int directionOffset;

        switch (state)
        {
            case ActorState.IDLE:
                frameIndex = 0;
                directionOffset = (int)direction * walkFrames;
                spriteRenderer.sprite = walkSprites[directionOffset + frameIndex];
                break;
            case ActorState.WALK:
                if (Time.time > nextFrameTime)
                {
                    frameIndex++;
                    frameIndex %= walkFrames;
                    nextFrameTime = Time.time + (1 / frameTime[(int)state]);
                }
                directionOffset = (int)direction * walkFrames;
                spriteRenderer.sprite = walkSprites[directionOffset + frameIndex];
                break;
            case ActorState.DANCE:
                if (Time.time > nextFrameTime)
                {
                    frameIndex++;
                    frameIndex %= danceSprites.Length;
                    nextFrameTime = Time.time + (1 / frameTime[(int)state]);
                }
                spriteRenderer.sprite = danceSprites[frameIndex];
                break;
            case ActorState.ATTACK:
                spriteRenderer.sprite = attackSprites[(int)direction];
                break;
        }

    }

    protected void FixedUpdate()
    {
        rigidbody2D.velocity = moveVector;
    }

    protected virtual void DoDance()
    {
        state = ActorState.DANCE;
        stateTimeoutTime = Time.time + danceTimeout;
        moveVector = Vector2.zero;
        nextFrameTime = Time.time + (1 / frameTime[(int)ActorState.DANCE]);
        FireDanceProjectiles();
        frameIndex = 0;
    }

    protected virtual void DoAttack()
    {
        state = ActorState.ATTACK;
        stateTimeoutTime = Time.time + attackTimeout;
        moveVector = Vector2.zero;
        nextFrameTime = Time.time + (1 / frameTime[(int)ActorState.ATTACK]);
        frameIndex = 0;
        FireAttackProjectiles();
    }

    protected virtual void FireDanceProjectiles()
    {
        StartCoroutine(FireDanceProjectilesWait());
    }

    protected virtual void FireAttackProjectiles()
    {
        Vector2 baseVector = Vector2.zero;
        switch (direction)
        {
            case Direction.NORTH:
                baseVector = Vector2.up;
                break;
            case Direction.EAST:
                baseVector = Vector2.right;
                break;
            case Direction.SOUTH:
                baseVector = Vector2.down;
                break;
            case Direction.WEST:
                baseVector = Vector2.left;
                break;
        }
        Projectile newProjectile = Instantiate(attackProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.SetBaseVector(baseVector);
        newProjectile.SetSourceObject(gameObject);
        // TODO: attack sound

    }
    protected virtual IEnumerator FireDanceProjectilesWait()
    {
        yield return new WaitForSeconds(1 / frameTime[(int)ActorState.DANCE]);
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
        // TODO: dance sound
    }
}
