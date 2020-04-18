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
    HIT = 4,
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
        nextFrameTime = Time.time + frameTime[(int)ActorState.WALK];
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
                    nextFrameTime = Time.time + frameTime[(int)state];
                }
                directionOffset = (int)direction * walkFrames;
                spriteRenderer.sprite = walkSprites[directionOffset + frameIndex];
                break;
            case ActorState.DANCE:
                if (Time.time > nextFrameTime)
                {
                    frameIndex++;
                    frameIndex %= danceSprites.Length;
                    nextFrameTime = Time.time + frameTime[(int)state];
                }
                spriteRenderer.sprite = danceSprites[frameIndex];
                break;
            case ActorState.ATTACK:
                break;
            case ActorState.HIT:
                break;
        }

    }

    protected void FixedUpdate()
    {
        rigidbody2D.velocity = moveVector;
    }

    protected virtual void FireDanceProjectiles()
    {

    }
}
