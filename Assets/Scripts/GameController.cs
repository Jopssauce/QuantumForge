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
    private LevelConfig levelConfig;
    SceneController sceneController;
    ActionRecorder actionRecorder;
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!actionRecorder.isPlaying)
            {
                actionRecorder.StopPlayback();
                actionRecorder.SaveRecording();
                actionRecorder.isPlaying = false;
                actionRecorder.isRecording = false;
                sceneController.ResetLevel(mainScene);
            }
        }
        //Reset
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (!actionRecorder.isPlaying)
            {
                actionRecorder.StopPlayback();
                actionRecorder.isPlaying = false;
                sceneController.ResetLevel(mainScene);
            }
        }
        //Record and Play
        if (Input.GetKeyDown(KeyCode.R))
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
        //Delete previous recording and reset
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (actionRecorder.actionsList.Count > 0)
            {
                actionRecorder.StopPlayback();
                actionRecorder.DeletePreviousRecording();
                actionRecorder.isPlaying = false;
                actionRecorder.isRecording = false;
                sceneController.ResetLevel(mainScene);
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

    
}
