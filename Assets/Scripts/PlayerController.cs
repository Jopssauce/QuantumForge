using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D characterController2D;

    private void Update()
    {
        characterController2D.MoveHorizontal(Input.GetAxis("Horizontal"));
        characterController2D.MoveVeritcal(Input.GetAxis("Vertical"));
    }
}
