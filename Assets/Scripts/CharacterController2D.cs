﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    float maxVelocity = 5f;
    [SerializeField]
    float maxAcceleration = 5f;

    float axisX;
    float axisY;
    [SerializeField]
    Vector2 currentVelocity;
    Vector2 targetVelocity;

    Rigidbody2D rgBody2D;

    private void Awake()
    {
        rgBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 input = new Vector2();
        input.x = axisX;
        input.y = axisY;
        input = Vector2.ClampMagnitude(input, 1f);

        targetVelocity = input * maxVelocity;
        currentVelocity = rgBody2D.velocity;

        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, maxAcceleration * Time.deltaTime);

        rgBody2D.velocity = currentVelocity;
    }

    //public IEnumerator MoveRight()
    //{
    //    MoveHorizontal(1);
    //    yield return new WaitForSeconds(1);
    //    MoveHorizontal(0);
    //}

    public void MoveHorizontal(float axis)
    {
        axisX = axis;
    }

    public void MoveVeritcal(float axis)
    {
        axisY = axis;
    }
}
