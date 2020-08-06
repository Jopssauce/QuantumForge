using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameController gameController;
    ActionRecorder actionRecorder;
    public List<Image> recordingSprites;
    public Image lifeLine;

    public Sprite filledSprite;
    public Sprite emptySprite;

    private void Start()
    {
        gameController = GameController.instance;
        actionRecorder = ActionRecorder.instance;
    }

    private void Update()
    {
        UpdateRecordingSprites();
        UpdateLifelineSprite();
    }

    public void UpdateRecordingSprites()
    {
        for (int i = 0; i < recordingSprites.Count; i++)
        {
            if (i <= actionRecorder.actionsList.Count)
            {
                recordingSprites[i].sprite = filledSprite;
            }
            else
            {
                recordingSprites[i].sprite = emptySprite;
            }
        }
    }

    public void UpdateLifelineSprite()
    {
        lifeLine.fillAmount =  ((float)actionRecorder.stepsLeft / (float)actionRecorder.totalSteps);
    }
}
