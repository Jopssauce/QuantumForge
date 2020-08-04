using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationOutput : StationIO
{
    public delegate void OnOutput();
    public event OnOutput onOutput;

    Item resultItemInfo;

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
        station.item = null;
        DisplayItem(resultItem);
        onOutput?.Invoke();
    }

    public void DisplayItem(Item resultItem)
    {
        itemSpriteRenderer.sprite = resultItem.sprite;
    }

    public void GiveItem(Item resultItem, Character character)
    {
        character.items.Add(resultItem);
        ClearOutput();
    }

    public void ClearOutput()
    {
        itemSpriteRenderer.sprite = null;
        resultItemInfo = null;
    }
}
