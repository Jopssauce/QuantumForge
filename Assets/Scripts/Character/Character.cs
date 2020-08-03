using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public CharacterController2D characterController;

    public bool isRecording;
    public bool isRewinding;
    public bool isPlaying;

    public float health = 20;
    public float maxRecordTime = 3f;
    public int totalSteps;

    int index;

    public List<Item> items;
    private List<CharacterAction> actions = new List<CharacterAction>();


    private void Awake()
    {
        index = 0;
        characterController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Record();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RewindRecording();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayRecording();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || 
            Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
        {
            if (isRecording)
            {
                RecordMovement();
            } 
        }

        if (isRecording)
        {
            if (actions.Count >= totalSteps)
            {
                isRecording = false;
            }
        }

        if (isRewinding)
        {
            if (index > 0)
            {
                CharacterAction action = actions[index];
                transform.position = action.position;
                characterController.facingDirection = action.facingDirection;
                index--;
            }
            else
            {
                isRewinding = false;
            }
            
        }

        if (isPlaying)
        {
            if (index < actions.Count)
            {
                CharacterAction action = actions[index];
                transform.position = action.position;
                characterController.facingDirection = action.facingDirection;
                index++;
            }
            else
            {
                isPlaying = false;
            }

        }
    }

    public void RewindRecording()
    {
        isRewinding = true;
        index = actions.Count - 1;
    }

    public void PlayRecording()
    {
        isPlaying = true;
        index = 0;
    }

    public void Record()
    {
        isRecording = true;
        totalSteps = Mathf.RoundToInt(maxRecordTime / Time.fixedDeltaTime);
    }

    public void RecordMovement()
    {
        CharacterAction action = new CharacterAction();
        action.SetMovement(transform.position, characterController.facingDirection);
        actions.Add(action);
    }

    public void RecordPickUp(Item item)
    {

    }

    public void RecordPlaceDown(Item item)
    {

    }

    public void RecordInsert(Item item, Station station)
    {
        
    }
}
