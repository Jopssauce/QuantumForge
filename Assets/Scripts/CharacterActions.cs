using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    Character character;

    public Item itemInRange;
    public Station stationInRange;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public enum Actions
    {
        Move,
        Pickup,
        PlaceDown
    }

    public void PickUp(Item item)
    {
        character.RecordPickUp(item);
    }

    public void PlaceDown(Item item)
    {
        character.RecordPlaceDown(item);
    }

    public void Insert(Item item, Station station)
    {
        character.RecordInsert(item, station);
    }
}
