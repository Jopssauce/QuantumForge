﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterAction
{
    public enum Actions
    {
        Move,
        Interact
    }

    public Actions action;
    //Movement
    public Vector2 position;
    public Vector2 facingDirection;

    public void SetAction(Actions action)
    {
        this.action = action;
    }
    public void SetMovement(Vector2 pos, Vector2 direction)
    {
        this.position = pos;
        this.facingDirection = direction;
    }
}
