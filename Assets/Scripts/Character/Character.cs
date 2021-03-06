﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public CharacterController2D characterController;

    public string pickupSound;
    public float rayDistance = 1;
    public Interactable interactable;

    public SpriteRenderer heldItem;
    AudioManager audioManager;

    public bool isMoving;
    Vector2 lastPosition;

    RaycastHit2D[] hit;
    Ray2D ray;

    public List<Item> items;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
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
                    return;
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

    private void FixedUpdate()
    {
        if ((Vector2)transform.position == lastPosition)
        {
            isMoving = false;
        }
        else if ((Vector2)transform.position != lastPosition)
        {
            isMoving = true;
        }
        lastPosition = transform.position;
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
            audioManager.PlaySFX(pickupSound);
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
