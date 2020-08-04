using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRecorder : MonoBehaviour
{
    public Character currentPlayer;
    [SerializeField]
    private Character characterPrefab = null;
    private CharacterController2D characterController;

    public float maxRecordTime = 3f;
    public bool isRecording;
    public bool isRewinding;
    public bool isPlaying;
    public int totalSteps;

    private List<CharacterAction> actions = new List<CharacterAction>();
    private List<List<CharacterAction>> actionsList = new List<List<CharacterAction>>();

    private void Awake()
    {
        characterController = currentPlayer.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Record();
        }
        if (Input.GetKeyDown(KeyCode.O) && !isRecording)
        {
            //RewindRecording();
            //Replace with Reset
        }
        if (Input.GetKeyDown(KeyCode.P) && !isRecording)
        {
            PlayAllRecordings();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isRecording)
        {
            SaveRecording();
        }
    }

    private void FixedUpdate()
    {
        if (isRecording)
        {
            if (actions.Count >= totalSteps)
            {
                isRecording = false;
            }
        }
    }


    public void RewindRecording()
    {
        isRewinding = true;
        StartCoroutine(RewindRecording(actions, currentPlayer));
    }

    public void PlayAllRecordings()
    {
        isPlaying = true;
        for (int i = 0; i < actionsList.Count; i++)
        {
            Character character;
            character = Instantiate(characterPrefab, characterPrefab.transform.position, characterPrefab.transform.rotation);
            StartCoroutine(PlayRecording(actionsList[i], character));
        }
    }

    public IEnumerator PlayRecording(List<CharacterAction> actions, Character character)
    {
        int index = 0;
        CharacterController2D characterController = character.GetComponent<CharacterController2D>();
        while (index < actions.Count)
        {
            if (index < actions.Count)
            {
                CharacterAction action = actions[index];
                character.transform.position = action.position;
                characterController.facingDirection = action.facingDirection;
                index++;

                if (action.action == CharacterAction.Actions.Interact)
                {
                    character.Interact(character.interactable);
                }
            }
            yield return new WaitForFixedUpdate();
        }
        isPlaying = false;
    }

    public IEnumerator RewindRecording(List<CharacterAction> actions, Character character)
    {
        int index = actions.Count - 1;
        CharacterController2D characterController = character.GetComponent<CharacterController2D>();
        while (index > 0)
        {
            if (index > 0)
            {
                //Rewind Movement
                CharacterAction action = actions[index];
                character.transform.position = action.position;
                characterController.facingDirection = action.facingDirection;
                index--;

                if (action.action == CharacterAction.Actions.Interact)
                {
                    character.Interact(character.interactable);
                }
            }
            yield return new WaitForFixedUpdate();
        }
        isRewinding = false;

    }

    public void Record()
    {
        isRecording = !isRecording;
        totalSteps = Mathf.RoundToInt(maxRecordTime / Time.fixedDeltaTime);
    }

    public void SaveRecording()
    {
        List<CharacterAction> newAction = new List<CharacterAction>(actions);
        actionsList.Add(newAction);
        actions.Clear();
    }

    public void RecordMovement()
    {
        if (isRecording)
        {
            CharacterAction action = new CharacterAction();
            action.SetAction(CharacterAction.Actions.Move);
            action.SetMovement(currentPlayer.transform.position, characterController.facingDirection);
            actions.Add(action);
        }

    }

    public void RecordInteract(Interactable interactable)
    {
        if (isRecording)
        {
            CharacterAction action = new CharacterAction();
            action.SetAction(CharacterAction.Actions.Interact);
            action.SetMovement(currentPlayer.transform.position, characterController.facingDirection);
            actions.Add(action);
        }
    }
}
