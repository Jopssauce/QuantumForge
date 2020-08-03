using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationOutput : StationIO
{
    public delegate void OnOutput();
    public event OnOutput onOutput;

    public void OutputItem(Item item)
    {

        onOutput?.Invoke();
    }

    public void DisplayItem()
    {

    }

    public void GiveItem(Item item, Character character)
    {

    }

    public void ClearOutput()
    {

    }
}
