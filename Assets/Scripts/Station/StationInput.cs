using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInput : MonoBehaviour
{
    public delegate void OnInput();
    public event OnInput onInput;

    public Station station;
    public Item itemInfo;

    public void InputItem(Item item)
    {
        //Check if output is full
        if (station.output.itemInfo != null)
        {
            return;
        }
        itemInfo = item;
        onInput?.Invoke();
    }
}
