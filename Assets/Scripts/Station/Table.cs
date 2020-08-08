using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Interactable
{
    [SerializeField]
    Item itemInfo;
    public SpriteRenderer itemSprite;

    private void Start()
    {
        if (itemInfo != null)
        {
            itemSprite.sprite = itemInfo.sprite;
        }
        else
        {
            itemSprite.sprite = null;
        }
    }

    public override void Interact(Character character)
    {
        base.Interact(character);

        if (character.items.Count > 0 && itemInfo == null)
        {
            Item item = character.items[0];
            PlaceItem(character, item);
        }
        else if (itemInfo != null && character.items.Count == 0)
        {
            GiveItem(character);
        }
    }

    //Place Item on Table
    void PlaceItem(Character character, Item item)
    {
        itemInfo = character.GiveCharacterItem();
        itemSprite.sprite = itemInfo.sprite;
    }

    //Gives item to character
    void GiveItem(Character character)
    {
        character.TakeItem(itemInfo);
        itemSprite.sprite = null;
        itemInfo = null;
    }
}
