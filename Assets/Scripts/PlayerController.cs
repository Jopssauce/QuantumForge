using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D characterController2D;
    public Character character;

    private void Update()
    {
        characterController2D.MoveHorizontal(Input.GetAxis("Horizontal"));
        characterController2D.MoveVeritcal(Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (character.interactable != null)
            {
                character.Interact(character.interactable);
                character.actionRecorder.RecordInteract(character.interactable);
            }
        }
    }


    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 ||
            Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
        {
            if (character.actionRecorder.isRecording)
            {
                character.actionRecorder.RecordMovement();
            }
        }
    }
}
