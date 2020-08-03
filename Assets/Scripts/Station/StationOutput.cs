using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationOutput : MonoBehaviour
{
    public delegate void OnOutput();
    public event OnOutput onOutput;

    public Station station;
    public Item itemInfo;
    public SpriteRenderer spriteRenderer;

    public void OutputItem()
    {
        spriteRenderer.sprite = itemInfo.sprite;
        onOutput?.Invoke();
    }
}
