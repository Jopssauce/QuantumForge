using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIOStation : Station
{
    private void Awake()
    {
        foreach (var input in inputs)
        {
            input.onInput += AcceptItem;
        }
    }

    //public void CreateResult()
    //{
    //    ItemRecipe itemRecipe = FindMatchingRecipe();
    //    if(itemRecipe != null) resultItem = itemRecipe.resultItem;
    //    if (resultItem != null)
    //    {
    //        OutputResult();
    //    }
    //    else
    //    {
    //        //Return Item
    //        input.DisplayItem(item);
    //        item = null;
    //    }
    //}

    public void AcceptItem()
    {
        //Find Matching Recipe
        //If recipe found process items
        //Craft result
        //Output the Result
        //Remove item from input
        ItemRecipe itemRecipe = FindMatchingRecipe();
        if (itemRecipe != null)
        {
            foreach (var input in inputs)
            {
                input.ClearInput();
            }
            output.OutputItem(itemRecipe.resultItem);
        }
    }

    //public void OutputResult()
    //{
    //    output.OutputItem(resultItem);
    //}
}
