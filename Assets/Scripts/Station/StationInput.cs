using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInput : StationIO
{
    public delegate void OnInput();
    public event OnInput onInput;

    public void InputItem(Item item)
    {

        onInput?.Invoke();
    }
}
