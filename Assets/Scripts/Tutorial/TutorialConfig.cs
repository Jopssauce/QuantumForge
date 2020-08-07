using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialConfig : MonoBehaviour
{
    public int index;
    public int maxPhases;

    //Phase 1
    public bool isPhase1Done;
    public delegate void OnPhase1Done();
    public event OnPhase1Done onPhase1Done;
    [TextArea]
    public string phase1Text;

    //Phase 2
    public bool isPhase2Done;
    public delegate void OnPhase2Done();
    public event OnPhase1Done onPhase2Done;
    [TextArea]
    public string phase2Text;

    //Phase 3
    public bool isPhase3Done;
    public delegate void OnPhase3Done();
    public event OnPhase1Done onPhase3Done;
    [TextArea]
    public string phase3Text;

    //Phase 4
    public bool isPhase4Done;
    public delegate void OnPhase4Done();
    public event OnPhase1Done onPhase4Done;
    [TextArea]
    public string phase4Text;

    //Phase 5
    public bool isPhase5Done;
    public delegate void OnPhase5Done();
    public event OnPhase1Done onPhase5Done;
    [TextArea]
    public string phase5Text;

    public IEnumerator Phase1(GameController gameController)
    {
        isPhase1Done = false;
        while (isPhase1Done == false)
        {
            if (Input.GetKeyDown(gameController.saveKey))
            {
                isPhase1Done = true;
                onPhase1Done?.Invoke();
            }
            yield return null;
        }
        yield break;
    }

    public IEnumerator Phase2(GameController gameController)
    {
        isPhase2Done = false;
        while (isPhase2Done == false)
        {
            if (Input.GetKeyDown(gameController.cancelKey))
            {
                isPhase2Done = true;
                onPhase2Done?.Invoke();
            }
            yield return null;
        }
        yield break;
    }

    public IEnumerator Phase3(GameController gameController)
    {
        isPhase3Done = false;
        while (isPhase3Done == false)
        {
            if (Input.GetKeyDown(gameController.redoKey))
            {
                isPhase3Done = true;
                onPhase3Done?.Invoke();
            }
            yield return null;
        }
        yield break;
    }

    public IEnumerator Phase4(GameController gameController)
    {
        isPhase4Done = false;
        while (isPhase4Done == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPhase4Done = true;
                onPhase4Done?.Invoke();
            }
            yield return null;
        }
        yield break;
    }

    public IEnumerator Phase5(GameController gameController)
    {
        isPhase5Done = false;
        while (isPhase5Done == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isPhase5Done = true;
                onPhase5Done?.Invoke();
            }
            yield return null;
        }
        yield break;
    }


}
