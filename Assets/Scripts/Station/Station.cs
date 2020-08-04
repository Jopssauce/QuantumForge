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
            if (UnorderedEqual(itemRecipe.components, items))
            {
                return itemRecipe;
            }
        }
        return null;
    }


    static bool UnorderedEqual<T>(ICollection<T> a, ICollection<T> b)
    {
        // 1
        // Require that the counts are equal
        if (a.Count != b.Count)
        {
            return false;
        }
        // 2
        // Initialize new Dictionary of the type
        Dictionary<T, int> d = new Dictionary<T, int>();
        // 3
        // Add each key's frequency from collection A to the Dictionary
        foreach (T item in a)
        {
            int c;
            if (d.TryGetValue(item, out c))
            {
                d[item] = c + 1;
            }
            else
            {
                d.Add(item, 1);
            }
        }
        // 4
        // Add each key's frequency from collection B to the Dictionary
        // Return early if we detect a mismatch
        foreach (T item in b)
        {
            int c;
            if (d.TryGetValue(item, out c))
            {
                if (c == 0)
                {
                    return false;
                }
                else
                {
                    d[item] = c - 1;
                }
            }
            else
            {
                // Not in dictionary
                return false;
            }
        }
        // 5
        // Verify that all frequencies are zero
        foreach (int v in d.Values)
        {
            if (v != 0)
            {
                return false;
            }
        }
        // 6
        // We know the collections are equal
        return true;
    }
}

