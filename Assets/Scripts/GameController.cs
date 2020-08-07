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

    public bool canMove = true;
    public bool canControl = true;

    [SerializeField]
    private Character characterPrefab = null;
    [SerializeField]
    private LevelConfig levelConfig = null;

    SceneController sceneController;
    ActionRecorder actionRecorder;

    public KeyCode saveKey      = KeyCode.I;
    public KeyCode cancelKey    = KeyCode.O;
    public KeyCode redoKey      = KeyCode.P;

    bool hasDied;

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
        if (actionRecorder.stepsIndex == actionRecorder.totalSteps && hasDied == false)
        {
            hasDied = true;
            onLifelineDepleted?.Invoke();
            canControl = false;
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
            actionRecorder.ResetRecorder();
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
