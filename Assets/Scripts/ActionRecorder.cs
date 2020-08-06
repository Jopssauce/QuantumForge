using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRecorder : MonoBehaviour
{
    public static ActionRecorder instance;

    public Character currentPlayer;
    private CharacterController2D characterController;
    public GameController gameController;
    public LevelConfig levelConfig;

    public float maxRecordTime = 3f;
    public bool isRecording;
    public bool isRewinding;
    public bool isPlaying;
    public int totalSteps;
    public int stepsIndex;
    public int stepsLeft;

    public int currentRecords;

    IEnumerator currentPlayingCoroutine;
    IEnumerator coroutineQueue;

    private List<CharacterAction> actions = new List<CharacterAction>();
    public List<List<CharacterAction>> actionsList = new List<List<CharacterAction>>();
    private List<IEnumerator> coroutines = new List<IEnumerator>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameController = GameController.instance;
    }

    private void Update()
    {
        stepsLeft = Mathf.Abs(stepsIndex - totalSteps);
    }

    private void FixedUpdate()
    {
        if (isRecording)
        {
            if (stepsIndex >= totalSteps)
            {
                isRecording = false;
            }
        }
    }

    public void LoadConfig(LevelConfig levelConfig)
    {
        this.levelConfig = levelConfig;
        maxRecordTime = levelConfig.maxRecordTime;
        currentRecords = levelConfig.recordAmount;
    }

    //Looks at the set player
    public void LookAt(Character player)
    {
        currentPlayer = player;
        characterController = player.GetComponent<CharacterController2D>();
    }

    public void RewindRecording()
    {
        isRewinding = true;
        StartCoroutine(RewindRecording(actions, currentPlayer));
    }

    public void StopPlayback()
    {
        if (currentPlayingCoroutine != null)
        {
            StopCoroutine(currentPlayingCoroutine);
            isPlaying = false;
        }
        if (coroutineQueue != null)
        {
            StopCoroutine(coroutineQueue);
            isPlaying = false;
        }
        if (coroutines.Count > 0)
        {
            for (int i = 0; i < coroutines.Count; i++)
            {
                StopCoroutine(coroutines[i]);
            }
        }
    }

    public void PlayAllRecordings()
    {
        if (actionsList.Count > 0)
        {
            isPlaying = true;
            PlayRecordingSimultaneously();
            //coroutineQueue = PlayRecordingsStepbyStep();
            //StartCoroutine(coroutineQueue);
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
        currentPlayingCoroutine = null;
    }

    public void PlayRecordingSimultaneously()
    {
        for (int i = 0; i < actionsList.Count; i++)
        {
            Character character;
            character = gameController.CreateCharacter(gameController.mainScene);
            IEnumerator cor;
            cor = PlayRecording(actionsList[i], character);
            coroutines.Add(cor);
            StartCoroutine(cor);
        }
    }

    public IEnumerator PlayRecordingsStepbyStep()
    {
        int coroutineIndex = 0;
        while (coroutineIndex < actionsList.Count)
        {
            if (currentPlayingCoroutine == null)
            {
                Character character;
                character = gameController.CreateCharacter(gameController.mainScene);
                currentPlayingCoroutine = PlayRecording(actionsList[coroutineIndex], character);
                StartCoroutine(currentPlayingCoroutine);
                coroutineIndex++;
            }
            yield return new WaitForFixedUpdate();
        }
        coroutineQueue = null;
        yield break;
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
        isRecording = true;
        totalSteps = Mathf.RoundToInt(maxRecordTime / Time.fixedDeltaTime);
    }

    public void StopRecording()
    {
        isRecording = false;
        actions.Clear();
    }

    public void SaveRecording()
    {
        List<CharacterAction> newAction = new List<CharacterAction>(actions);
        actionsList.Add(newAction);
        actions.Clear();
        ResetSteps();
    }

    public void DeletePreviousRecording()
    {
        actionsList.RemoveAt(actionsList.Count - 1);
    }

    public void RecordPosition()
    {
        if (isRecording)
        {
            CharacterAction action = new CharacterAction();
            action.SetAction(CharacterAction.Actions.Move);
            action.SetMovement(currentPlayer.transform.position, characterController.facingDirection);
            actions.Add(action);
        }

    }

    public void RecordMovement()
    {
        if (isRecording)
        {
            CharacterAction action = new CharacterAction();
            action.SetAction(CharacterAction.Actions.Move);
            action.SetMovement(currentPlayer.transform.position, characterController.facingDirection);
            actions.Add(action);
            stepsIndex++;
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
            stepsIndex++;
        }
    }

    public void ResetRecorder()
    {
        ResetSteps();
        actions.Clear();
        actionsList.Clear();
    }

    public void CleanRecorder()
    {
        ResetSteps();
        actions.Clear();
    }

    public void ResetSteps()
    {
        totalSteps = 0;
        stepsIndex = 0;
        stepsLeft = 0;
    }
}
