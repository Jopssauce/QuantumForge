using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterAction
{
    public CharacterActions.Actions action;
    //Movement
    public Vector2 position;
    public Vector2 facingDirection;

    public void SetAction(CharacterActions.Actions action)
    {
        this.action = action;
    }
    public void SetMovement(Vector2 pos, Vector2 direction)
    {
        action = CharacterActions.Actions.Move;
        this.position = pos;
        this.facingDirection = direction;
    }
}
