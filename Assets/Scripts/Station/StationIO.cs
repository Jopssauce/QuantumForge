using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationIO : Interactable
{
    public enum IOType
    {
        Input,
        Output,
        Both
    }
    public IOType stationIOType;
    public SpriteRenderer spriteRenderer;

    public delegate void OnGiveItem();
    public event OnGiveItem onGiveItem;

    public Station station;
    public Item itemInfo;

}
