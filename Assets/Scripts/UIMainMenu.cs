using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    SceneController sceneController;
    public void Start()
    {
        sceneController = SceneController.instance;
    }

    public void StartGame()
    {
        sceneController.ReplaceLevel("Main Menu", "Stage 0");
    }
}
