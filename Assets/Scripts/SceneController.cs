using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    AsyncOperation async;
    AsyncOperation async2;

    private void Awake()
    {
        instance = this;
    }

    public void ResetLevel(string scene)
    {
        async = SceneManager.UnloadSceneAsync(scene);
        async.completed += delegate { SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive); };
    }

    public void ReplaceLevel(string currentScene, string newScene)
    {
        async2 = SceneManager.UnloadSceneAsync(currentScene);
        async2.completed += delegate { SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive); };

    }
}
