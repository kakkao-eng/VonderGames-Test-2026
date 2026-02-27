using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageChest : MonoBehaviour
{
    public string chestName = "Storage Chest";
    public int storageSize = 30;
    public List<InventorySlot> storageSlots = new List<InventorySlot>();

    private void Awake()
    {
        // เริ่มต้นหีบด้วยช่องว่างเปล่า 30 ช่อง
        for (int i = 0; i < storageSize; i++)
        {
            storageSlots.Add(new InventorySlot(null, 0));
        }
    }

    public void OpenChest()
    {
        if (StorageUI.Instance != null)
        {
            StorageUI.Instance.OpenStorage(this);
        }
    }
}
