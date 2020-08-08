using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositBox : Interactable
{
    [SerializeField]
    Item itemInfo = null;
    [SerializeField]
    Item correctItem = null;
    public SpriteRenderer itemSprite;

    GameController gameController;

    private void Start()
    {
        gameController = GameController.instance;
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
        if (item.id == correctItem.id)
        {
            ProcWinCondition();
        }
    }

    //Gives item to character
    void GiveItem(Character character)
    {
        character.TakeItem(itemInfo);
        itemSprite.sprite = null;
        itemInfo = null;
    }

    void ProcWinCondition()
    {
        gameController.WinLevel();
    }
}
