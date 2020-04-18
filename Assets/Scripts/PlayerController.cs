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

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Direction direction = Direction.SOUTH;

    [SerializeField]
    private int frameIndex = 0;
    [SerializeField]
    private Sprite[] walkSprites; // create in order NESW

    private int walkFrames;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        walkFrames = walkSprites.Length / 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int directionOffset = (int)direction * walkFrames;
        spriteRenderer.sprite = walkSprites[directionOffset];
    }
}
