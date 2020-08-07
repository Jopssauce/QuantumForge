using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public bool LoadMenuFirst;
    public GameObject tutorial;

    AsyncOperation async;
    AsyncOperation async2;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (LoadMenuFirst)
        {
            SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Additive);
        }
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
