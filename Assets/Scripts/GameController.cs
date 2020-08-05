﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Character player;
    [SerializeField]
    private Character characterPrefab = null;
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
                sceneController.ResetLevel("Main");
            }
        }
        //Reset
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (!actionRecorder.isPlaying)
            {
                actionRecorder.StopPlayback();
                actionRecorder.isPlaying = false;
                sceneController.ResetLevel("Main");
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
