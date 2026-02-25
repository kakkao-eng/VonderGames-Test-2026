using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty => item == null;

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void Clear()
    {
        item = null;
        quantity = 0;
    }
}
