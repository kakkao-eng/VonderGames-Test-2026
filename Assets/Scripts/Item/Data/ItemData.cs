using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Resource,
    Tool,
    Seed,
    Crafted
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxStack = 10;
}
