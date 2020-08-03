using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    public CharacterController2D characterController;
    Vector2 facingDirection;

    SpriteRenderer spriteRenderer;
    public Sprite front;
    public Sprite back;
    public Sprite left;
    public Sprite right;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    private void Update()
    {
        facingDirection = characterController.facingDirection;
        if (facingDirection.x > 0)
        {
            spriteRenderer.sprite = right;
        }
        if (facingDirection.x < 0)
        {
            spriteRenderer.sprite = left;
        }

        if (facingDirection.y > 0)
        {
            spriteRenderer.sprite = front;
        }
        if (facingDirection.y < 0)
        {
            spriteRenderer.sprite = back;
        }
    }
}
