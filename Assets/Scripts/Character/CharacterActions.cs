using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    Character character;
    public float rayDistance = 1;
    public Item itemInRange;
    public Station stationInRange;

    RaycastHit2D[] hit;
    Ray2D ray;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        RaycastHit2D[] hit;
        ray = new Ray2D(transform.position, character.characterController.facingDirection);
        hit = Physics2D.RaycastAll(ray.origin, ray.direction, rayDistance);
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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
