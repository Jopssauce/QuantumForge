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
    public SpriteRenderer itemSpriteRenderer;

    public Station station;

}
