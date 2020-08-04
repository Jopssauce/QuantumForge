using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Station : Interactable
{
    [Header("Result Item")]
    public Item resultItem;

    //public Item item;
    //[SerializeField]
    //private Item itemRecipe = null;
    //public StationInput input;
    public StationOutput output;

    public List<Item> items;
    [SerializeField]
    private List<ItemRecipe> itemRecipes = new List<ItemRecipe>();
    public List<StationInput> inputs;
    //public List<StationOutput> outputs;

    //For Single Components
    //public bool IsItemCorrect()
    //{
    //    for (int i = 0; i < itemRecipes.Count; i++)
    //    {
    //        if (item.id == itemRecipes[i].component.id)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    return false;
    //}

    public ItemRecipe FindMatchingRecipe()
    {
        ItemRecipe itemRecipe = null;
        for (int i = 0; i < itemRecipes.Count; i++)
        {
            itemRecipe = itemRecipes[i];
            int index = 0;
            for (int w = 0; w < items.Count; w++)
            {
                if (itemRecipe.components.Any(a => a.id == items[i].id))
                {
                    index++;
                }
                if (index >= items.Count)
                {
                    return itemRecipe;
                }
            }
        }
        return null;
    }

}
