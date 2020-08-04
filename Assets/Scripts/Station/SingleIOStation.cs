using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIOStation : Station
{
    private void Awake()
    {
        input.onInput += CreateResult;
    }

    public void CreateResult()
    {
        if (IsItemCorrect())
        {
            OutputResult();
        }
        else
        {
            //Return Item
            input.DisplayItem(item);
            item = null;
        }
    }

    public void OutputResult()
    {
        output.OutputItem(resultItem);
    }
}
