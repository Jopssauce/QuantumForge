using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationOutput : StationIO
{
    public delegate void OnOutput();
    public event OnOutput onOutput;
    AudioManager audioManager;
    Item resultItemInfo;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    //Gives Result Item to Player
    public override void Interact(Character character)
    {
        base.Interact(character);
        if (resultItemInfo != null)
        {
            GiveItem(resultItemInfo, character);
        }
    }

    public void OutputItem(Item resultItem)
    {
        //Outputs result item
        resultItemInfo = resultItem;
        audioManager.PlaySFX(station.stationSound);
        //station.item = null;
        station.items.Clear();
        DisplayItem(resultItem);
        onOutput?.Invoke();
    }

    public void DisplayItem(Item resultItem)
    {
        itemSpriteRenderer.sprite = resultItem.sprite;
    }

    public void GiveItem(Item resultItem, Character character)
    {
        character.TakeItem(resultItem);
        ClearOutput();
    }

    public void ClearOutput()
    {
        itemSpriteRenderer.sprite = null;
        resultItemInfo = null;
        station.resultItem = null;
    }
}
