﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        instance = this;
    }

    public void ResetLevel(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }

    public void ReplaceLevel(string currentScene, string newScene)
    {
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
    }
}
