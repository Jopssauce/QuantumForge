using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Item Recipe/Recipe", order = 1)]
public class ItemRecipe : ScriptableObject
{
    public string recipeName;
    //For single component
    public Item component;
    //For Multiple Components
    public List<Item> components;
    public Item resultItem;
}
