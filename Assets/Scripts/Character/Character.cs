using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public CharacterController2D characterController;
    public ActionRecorder actionRecorder;

    public float health = 20;
    public float rayDistance = 1;
    public Interactable interactable;

    RaycastHit2D[] hit;
    Ray2D ray;

    public List<Item> items;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (interactable != null)
            {
                Interact(interactable);
            }
        }

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
        interactable.Interact(this);
        actionRecorder.RecordInteract(interactable);
    }

    //Gives Player item to an interactable
    public Item GiveCharacterItem()
    {
        if (items.Count > 0)
        {
            Item item = items[0];
            items.RemoveAt(0);
            return item;
        }
        return null;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
