using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    protected Direction direction = Direction.SOUTH;

    [SerializeField]
    protected int frameIndex = 0;
    [SerializeField]
    protected Sprite[] walkSprites; // create in order NESW

    protected int walkFrames;
    protected SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        walkFrames = walkSprites.Length / 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SetSprite();
    }

    protected void SetSprite()
    {
        int directionOffset = (int)direction * walkFrames;
        spriteRenderer.sprite = walkSprites[directionOffset + frameIndex];
    }
}
