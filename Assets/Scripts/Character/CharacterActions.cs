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

    public Station stationInRange;
    public StationOutput outputInRange;
    public StationInput inputInRange;

    RaycastHit2D[] hit;
    Ray2D ray;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (outputInRange != null)
            {
                if (outputInRange.itemInfo != null)
                {
                    PickUp(outputInRange);
                    return;
                }
            }
            if (inputInRange != null)
            {
                if (inputInRange.itemInfo == null)
                {
                    PlaceDown(inputInRange);
                    return;
                }
            }
        }

        RaycastHit2D[] hit;
        ray = new Ray2D(transform.position, character.characterController.facingDirection);
        hit = Physics2D.RaycastAll(ray.origin, ray.direction, rayDistance);
        if (hit.Length > 0)
        {
            foreach (var result in hit)
            {
                if (result.collider.GetComponent<StationOutput>() != null)
                {
                    outputInRange = result.collider.GetComponent<StationOutput>();
                }
                else if(result.collider.GetComponent<StationInput>() != null)
                {
                    inputInRange = result.collider.GetComponent<StationInput>();
                }
            }
        }
        else
        {
            outputInRange = null;
            inputInRange = null;
        }
    }

 
    public void PickUp(StationOutput station)
    {
        if (station.itemInfo != null)
        {
            Item item = station.itemInfo;
            station.itemInfo = null;
            station.spriteRenderer.sprite = null;

            character.items.Add(item);
        }
    }

    public void PlaceDown(StationInput station)
    {
        if (character.items.Count > 0)
        {
            station.InputItem(character.items[0]);
            character.items.RemoveAt(0);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
