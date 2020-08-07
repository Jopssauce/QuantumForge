using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    GameController gameController;
    ActionRecorder actionRecorder;

    public GameObject TextPanel;
    public TMPro.TextMeshProUGUI textBox;

    public void ToggleTextPanel()
    {
        TextPanel.SetActive(!TextPanel.activeSelf);
    }

    public void UpdateTextBox(string text)
    {
        textBox.text = text;
    }
}
