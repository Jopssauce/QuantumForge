using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public delegate void OnLifelineDepleted();
    public event OnLifelineDepleted onLifelineDepleted;

    public delegate void OnWin();
    public event OnWin onWin;

    public static GameController instance { get; private set; }
    public string mainScene;
    public Character player;

    public bool canControl = true;
    public bool isRecipeOpen = false;

    [SerializeField]
    private Character characterPrefab = null;
    [SerializeField]
    private LevelConfig levelConfig = null;

    SceneController sceneController;
    ActionRecorder actionRecorder;

    public bool isTutorial = false;

    public KeyCode saveKey      = KeyCode.I;
    public KeyCode cancelKey    = KeyCode.O;
    public KeyCode redoKey      = KeyCode.P;

    bool hasDied;
    bool hasWon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sceneController = SceneController.instance;
        actionRecorder = ActionRecorder.instance;

        actionRecorder.gameController = this;
        actionRecorder.LookAt(player);

        if (isTutorial)
        {
            sceneController.tutorial.gameObject.SetActive(true);
        }

        //If there is no level config or mismatching config
        if (actionRecorder.levelConfig == null || levelConfig.levelId != actionRecorder.levelConfig.levelId)
        {
            actionRecorder.LoadConfig(levelConfig);
        }

        if (!actionRecorder.isRecording)
        {
            actionRecorder.PlayAllRecordings();
            actionRecorder.Record();
        }
        else
        {
            actionRecorder.StopRecording();
        }
    }

    private void Update()
    {
        if (hasWon == true)
        {
            canControl = false;
            player.GetComponent<PlayerController>().canMove = false;
            return;
        }
        if (actionRecorder.stepsIndex == actionRecorder.totalSteps && hasDied == false)
        {
            hasDied = true;
            onLifelineDepleted?.Invoke();
            canControl = false;
            player.GetComponent<PlayerController>().canMove = false;
            return;
        }

        if (isRecipeOpen || hasWon == true || hasDied == true)
        {
            canControl = false;
            player.GetComponent<PlayerController>().canMove = false;
            actionRecorder.isRecording = false;
        }
        else if (!isRecipeOpen && hasDied == false)
        {
            canControl = true;
            player.GetComponent<PlayerController>().canMove = true;
            actionRecorder.isRecording = true;
        }
        else if (!isRecipeOpen && hasWon == false)
        {
            canControl = true;
            player.GetComponent<PlayerController>().canMove = true;
            actionRecorder.isRecording = true;
        }

        if (canControl)
        {
            //Save and Reset
            if (Input.GetKeyDown(saveKey))
            {
                SaveReset();
            }
            if (Input.GetKeyDown(cancelKey))
            {
                Cancel();
            }
            if (Input.GetKeyDown(redoKey))
            {
                Redo();
            }
        }

    }

    //Characters character into a specific scene
    public Character CreateCharacter(string scene)
    {
        Character character = 
            Instantiate(characterPrefab, characterPrefab.transform.position, characterPrefab.transform.rotation);
        SceneManager.MoveGameObjectToScene(character.gameObject, SceneManager.GetSceneByName(scene));
        return character;
    }

    public void ResetAll()
    {
        actionRecorder.StopRecording();
        actionRecorder.StopPlayback();
        actionRecorder.ResetRecorder();
        actionRecorder.CleanRecorder();
        actionRecorder.isPlaying = false;
        actionRecorder.isRecording = false;
        sceneController.ResetLevel(mainScene);
    }

    public void SaveReset()
    {
        if (actionRecorder.currentRecords > 0)
        {
            actionRecorder.currentRecords--;
            actionRecorder.StopPlayback();
            actionRecorder.SaveRecording();
            actionRecorder.isPlaying = false;
            actionRecorder.isRecording = false;
            sceneController.ResetLevel(mainScene);
        }
    }

    public void Redo()
    {
        if (actionRecorder.actionsList.Count > 0)
        {
            actionRecorder.currentRecords++;
            actionRecorder.StopRecording();
            actionRecorder.StopPlayback();
            actionRecorder.DeletePreviousRecording();
            actionRecorder.CleanRecorder();
            actionRecorder.isPlaying = false;
            actionRecorder.isRecording = false;
            sceneController.ResetLevel(mainScene);
        }
    }

    public void Cancel()
    {
        actionRecorder.StopRecording();
        actionRecorder.StopPlayback();
        actionRecorder.CleanRecorder();
        actionRecorder.isPlaying = false;
        actionRecorder.isRecording = false;
        sceneController.ResetLevel(mainScene);
    }

    public void RecordAndPlay()
    {
        if (!actionRecorder.isRecording)
        {
            actionRecorder.PlayAllRecordings();
            actionRecorder.Record();
        }
        else
        {
            actionRecorder.StopRecording();
        }
    }

    public void WinLevel()
    {
        hasWon = true;
        canControl = false;
        player.GetComponent<PlayerController>().canMove = false;
        onWin?.Invoke();
    }

    public void LoadNextLevel()
    {
        actionRecorder.StopRecording();
        actionRecorder.StopPlayback();
        actionRecorder.ResetRecorder();
        actionRecorder.CleanRecorder();
        sceneController.ReplaceLevel(mainScene, levelConfig.nextLevelName);
    }

    
}
