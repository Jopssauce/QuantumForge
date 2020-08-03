using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public enum Actions
    {
        Move,
        Pickup,
        PlaceDown
    }

    Character character;
    public float rayDistance = 1;

    //public Station stationInRange;
    //public StationOutput outputInRange;
    //public StationInput inputInRange;
    //public StationIO stationIOInRange;

    public Interactable interactable;

    RaycastHit2D[] hit;
    Ray2D ray;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            if (interactable != null)
            {
                Interact(interactable);
            }
        }

        RaycastHit2D[] hit;
        ray = new Ray2D(transform.position, character.characterController.facingDirection);
        hit = Physics2D.RaycastAll(ray.origin, ray.direction, rayDistance);
        if (hit.Length > 0)
        {
            foreach (var result in hit)
            {
                if (result.collider.GetComponent<Interactable>() != null)
                {
                    interactable = result.collider.GetComponent<Interactable>();
                }
            }
        }
        else
        {
            interactable = null;
            interactable = null;
        }
    }

    public void Interact(Interactable interactable)
    {
        
    }
 
    //public void TakeItem(StationIO station)
    //{
    //    if (station.itemInfo != null)
    //    {
    //        Item item = station.GiveItem();
    //        character.items.Add(item);
    //        character.RecordPickUp(item);
    //    }
    //}

    //public void Input(StationIO station)
    //{
    //    if (character.items.Count > 0)
    //    {
    //        station.InputItem(character.items[0]);
    //        character.RecordPlaceDown(character.items[0]);
    //        character.items.RemoveAt(0);
    //    }
    //}

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
