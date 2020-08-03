﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIOStation : Station
{
    private void Awake()
    {
        input.onInput += DisplayItem;
    }

    public void DisplayItem()
    {
        output.itemInfo = input.itemInfo;
        input.itemInfo = null;
        output.OutputItem();
    }
}
