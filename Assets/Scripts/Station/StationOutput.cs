using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationOutput : StationIO
{
    public delegate void OnOutput();
    public event OnOutput onOutput;
    AudioManager audioManager;
    Item resultItemInfo;

    public GameObject outputParticle;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    //Gives Result Item to Player
    public override void Interact(Character character)
    {
        base.Interact(character);
        if (resultItemInfo != null && character.items.Count == 0)
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
        Instantiate(outputParticle, itemSpriteRenderer.transform.position, outputParticle.transform.rotation);
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
