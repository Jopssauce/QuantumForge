using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Item/TestItem", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public int id;
    public Sprite sprite;
}
