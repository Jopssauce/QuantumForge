using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D characterController2D;
    public Character character;
    public ActionRecorder actionRecorder;

    public bool canMove = true;

    private void Start()
    {
        actionRecorder = ActionRecorder.instance;
    }

    private void Update()
    {
       

        if (canMove)
        {
            characterController2D.MoveHorizontal(Input.GetAxisRaw("Horizontal"));
            characterController2D.MoveVeritcal(Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (character.interactable != null)
                {
                    character.Interact(character.interactable);
                    actionRecorder.RecordInteract(character.interactable);
                }
            }
        }
        else
        {
            characterController2D.MoveHorizontal(0);
            characterController2D.MoveVeritcal(0);
        }

    }


    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 ||
            Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
        {
            if (actionRecorder.isRecording)
            {
                actionRecorder.RecordMovement();
            }
        }
        else if(Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0 ||
                Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            if (actionRecorder.isRecording)
            {
                actionRecorder.RecordPosition();
            }
        }
    }
}
