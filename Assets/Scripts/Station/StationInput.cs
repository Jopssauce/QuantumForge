using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInput : StationIO
{
    public delegate void OnInput();
    public event OnInput onInput;

    Item itemInfo;
    //Input Item into station
    public override void Interact(Character character)
    {
        base.Interact(character);
        if (itemInfo != null)
        {
            GiveItem(itemInfo, character);
        }
        else if (character.items.Count > 0 && station.item == null)
        {
            InputItem(character.GiveCharacterItem());
        }
    }

    public void InputItem(Item item)
    {
        station.item = item;
        onInput?.Invoke();
    }

    public void DisplayItem(Item item)
    {
        itemInfo = item;
        itemSpriteRenderer.sprite = item.sprite;
    }

    public void GiveItem(Item item, Character character)
    {
        character.items.Add(item);
        ClearInput();
    }

    public void ClearInput()
    {
        itemSpriteRenderer.sprite = null;
        itemInfo = null;
    }
}
