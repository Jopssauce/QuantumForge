using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public UITutorial uITutorial;
    GameController gameController;
    ActionRecorder actionRecorder;
    IEnumerator currentCoroutine;

    public TutorialConfig config;

    public bool isTrigger;

    private void Start()
    {
        gameController  = GameController.instance;
        actionRecorder  = ActionRecorder.instance;

        StartCoroutine(LateStart(0.2f));
    }

    private void Update()
    {
        if (gameController == null)
        {
            gameController = GameController.instance;
        }
        if (gameController.isTutorial == false)
        {
            uITutorial.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Debug.Log(true);
        }
        //I dunno why but touching this will crash unity and make me go insane
        if (gameController.isTutorial && isTrigger == false)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                actionRecorder.Record();
                isTrigger = true;
            }
        }
    }

    public void StartNextConfig()
    {
        if (config.index < config.maxPhases)
        {
            switch (config.index)
            {
                case 0:
                    uITutorial.TextPanel.SetActive(true);
                    config.onPhase1Done += EndConfig;
                    InitiateConfig(config.Phase1(gameController));
                    uITutorial.UpdateTextBox(config.phase1Text);
                    break;
                case 1:
                    uITutorial.TextPanel.SetActive(true);
                    config.onPhase2Done += EndConfig;
                    InitiateConfig(config.Phase2(gameController));
                    uITutorial.UpdateTextBox(config.phase2Text);
                    break;
                case 2:
                    uITutorial.TextPanel.SetActive(true);
                    config.onPhase3Done += EndConfig;
                    InitiateConfig(config.Phase3(gameController));
                    uITutorial.UpdateTextBox(config.phase3Text);
                    break;
                case 3:
                    uITutorial.TextPanel.SetActive(true);
                    config.onPhase4Done += EndConfig;
                    InitiateConfig(config.Phase4(gameController));
                    uITutorial.UpdateTextBox(config.phase4Text);
                    break;
                case 4:
                    uITutorial.TextPanel.SetActive(true);
                    config.onPhase5Done += EndConfig;
                    InitiateConfig(config.Phase5(gameController));
                    uITutorial.UpdateTextBox(config.phase5Text);
                    break;
                default:
                    break;
            }
            config.index++;
        }
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (gameController.isTutorial)
        {
            actionRecorder.isRecording = false;
            StartNextConfig();
        }
    }

    public void InitiateConfig(IEnumerator phase)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = phase;
        StartCoroutine(currentCoroutine);
    }

    public void EndConfig()
    {
        uITutorial.ToggleTextPanel();
        StartNextConfig();
    }

}
