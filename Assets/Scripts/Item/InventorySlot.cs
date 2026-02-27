using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public bool IsEmpty => item == null || quantity <= 0;

    public void Clear()
    {
        item = null;
        quantity = 0;
    }

    // --- เพิ่มฟังก์ชันนี้ลงไปเพื่อให้หายแดง ---
    public void CopyFrom(InventorySlot otherSlot)
    {
        this.item = otherSlot.item;
        this.quantity = otherSlot.quantity;
    }
}
