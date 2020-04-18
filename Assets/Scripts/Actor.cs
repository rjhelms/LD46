using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected Vector2 moveVector;

    [SerializeField]
    protected Direction direction = Direction.SOUTH;

    [SerializeField]
    protected int frameIndex = 0;
    [SerializeField]
    protected Sprite[] walkSprites; // create in order NESW

    protected int walkFrames;
    protected SpriteRenderer spriteRenderer;
    protected new Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        walkFrames = walkSprites.Length / 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        int directionOffset = (int)direction * walkFrames;
        spriteRenderer.sprite = walkSprites[directionOffset + frameIndex];
    }

    protected void FixedUpdate()
    {
        rigidbody2D.velocity = moveVector;
    }
}
