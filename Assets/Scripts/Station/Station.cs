using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : Interactable
{
    [Header("Result Item")]
    public Item resultItem;

    public Item item;
    [SerializeField]
    private Item itemRecipe = null;
    public StationInput input;
    public StationOutput output;

    public List<Item> items;
    [SerializeField]
    private List<Item> itemRecipes;
    public List<StationInput> inputs;
    public List<StationOutput> outputs;

    public bool IsItemCorrect()
    {
        if (item.id == itemRecipe.id)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AreItemsCorrect()
    {
        //Check list
        return false;
    }

}
