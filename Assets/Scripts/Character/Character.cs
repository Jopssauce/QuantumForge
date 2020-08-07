using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public CharacterController2D characterController;

    public float health = 20;
    public float rayDistance = 1;
    public Interactable interactable;

    public SpriteRenderer heldItem;

    RaycastHit2D[] hit;
    Ray2D ray;

    public List<Item> items;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        RaycastHit2D[] hit;
        ray = new Ray2D(transform.position, characterController.facingDirection);
        hit = Physics2D.RaycastAll(ray.origin, ray.direction, rayDistance);
        if (hit.Length > 0)
        {
            foreach (var result in hit)
            {
                if (result.collider.GetComponent<Interactable>() != null)
                {
                    interactable = result.collider.GetComponent<Interactable>();
                }
                else
                {
                    interactable = null;
                }
            }
        }
        else
        {
            interactable = null;
        }
    }

    public void Interact(Interactable interactable)
    {
        if (interactable == null)
        {
            Debug.Log(interactable, this);
        }
        interactable.Interact(this); 
    }

    //Gives Player item to an interactable
    public Item GiveCharacterItem()
    {
        if (items.Count > 0)
        {
            Item item = items[0];
            items.RemoveAt(0);
            heldItem.sprite = null;
            return item;
        }
        return null;
    }

    public void TakeItem(Item item)
    {
        if (items.Count < 1)
        {
            heldItem.sprite = item.sprite;
            items.Add(item);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
