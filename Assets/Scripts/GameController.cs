using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public string mainScene;
    public Character player;

    [SerializeField]
    private Character characterPrefab = null;
    [SerializeField]
    private LevelConfig levelConfig = null;

    SceneController sceneController;
    ActionRecorder actionRecorder;

    public KeyCode saveKey      = KeyCode.I;
    public KeyCode cancelKey    = KeyCode.O;
    public KeyCode redoKey      = KeyCode.P;

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
        //Reset
        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    if (!actionRecorder.isPlaying)
        //    {
        //        actionRecorder.StopPlayback();
        //        actionRecorder.isPlaying = false;
        //        sceneController.ResetLevel(mainScene);
        //    }
        //}
        ////Record and Play
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    RecordAndPlay();
        //}
        //Delete previous recording and reset

    }

    //Characters character into a specific scene
    public Character CreateCharacter(string scene)
    {
        Character character = 
            Instantiate(characterPrefab, characterPrefab.transform.position, characterPrefab.transform.rotation);
        SceneManager.MoveGameObjectToScene(character.gameObject, SceneManager.GetSceneByName(scene));
        return character;
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

    

    
}
