using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int inventorySize = 40;

    public List<InventorySlot> slots = new List<InventorySlot>();

    public Action OnInventoryChanged;

    [Header("Drag & Drop")]
    public Image dragIcon; // ลาก DragIcon จาก Hierarchy มาใส่
    private int draggingIndex = -1;

    [Header("Starting Items")]
    public List<ItemData> startingItems = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializeInventory();
    }

    void InitializeInventory()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot(null, 0));
        }
    }

    public bool AddItem(ItemData itemToAdd, int amount)
    {
        // เช็ค Stack เดิมก่อน
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == itemToAdd)
            {
                int spaceLeft = slot.item.maxStack - slot.quantity;
                if (spaceLeft > 0)
                {
                    int amountToAdd = Mathf.Min(spaceLeft, amount);
                    slot.quantity += amountToAdd;
                    amount -= amountToAdd;
                    if (amount <= 0) { OnInventoryChanged?.Invoke(); return true; }
                }
            }
        }
        // ถ้าเหลือ หาช่องว่างใหม่
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                int amountToAdd = Mathf.Min(itemToAdd.maxStack, amount);
                slot.item = itemToAdd;
                slot.quantity = amountToAdd;
                amount -= amountToAdd;
                if (amount <= 0) { OnInventoryChanged?.Invoke(); return true; }
            }
        }
        OnInventoryChanged?.Invoke();
        return false;
    }

    public bool RemoveItem(ItemData itemToRemove, int amount)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == itemToRemove)
            {
                if (slot.quantity >= amount)
                {
                    slot.quantity -= amount;

                    if (slot.quantity <= 0)
                        slot.Clear();

                    OnInventoryChanged?.Invoke();
                    return true;
                }
                else
                {
                    amount -= slot.quantity;
                    slot.Clear();
                }
            }
        }

        OnInventoryChanged?.Invoke();
        return false;
    }


    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count) return;

        InventorySlot slot = slots[slotIndex];
        if (slot == null || slot.IsEmpty || slot.item == null)
        {
            Debug.Log("ช่องนี้ว่างเปล่า ใช้งานไม่ได้");
            return;
        }

        ItemData item = slot.item;

        // --- ส่วนที่เพิ่มเพื่อ Scalability ---
        switch (item.itemType)
        {
            case ItemType.Tool:
                Debug.Log($"[Equip] กำลังถือเครื่องมือ: {item.itemName}");
                // ตรงนี้อนาคตสามารถส่งข้อมูลไปที่สคริปต์ Player เพื่อเปลี่ยน Sprite ในมือได้
                break;

            case ItemType.Seed:
                Debug.Log($"[Place] ทำการปลูก: {item.itemName}");
                slot.quantity--; // เมล็ดใช้แล้วลดลง
                break;
            case ItemType.Crafted:
                if (item.prefabToPlace != null)
                {
                    // ตรวจสอบว่า PlacementManager ถูกสร้างในฉากหรือยัง
                    if (PlacementManager.Instance != null)
                    {
                        bool success = PlacementManager.Instance.PlaceItem(item.prefabToPlace);
                        if (success) slot.quantity--;
                    }
                    else
                    {
                        Debug.LogError("หา PlacementManager ไม่เจอในฉาก!");
                    }
                }
                break;

            case ItemType.Resource:
                Debug.Log($"{item.itemName} เป็นทรัพยากร ใช้โดยตรงไม่ได้");
                return; // ไม่ต้องอัปเดต UI ถ้าไม่มีการเปลี่ยนแปลง
        }

        // ล้างช่องถ้าของหมด
        if (slot.quantity <= 0) slot.Clear();

        OnInventoryChanged?.Invoke();
    }
    public void SwapSlots(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex < 0 || fromIndex >= slots.Count || toIndex >= slots.Count) return;

        // สลับแค่ข้อมูลข้างใน (Item และ จำนวน)
        ItemData tempItem = slots[fromIndex].item;
        int tempQty = slots[fromIndex].quantity;

        slots[fromIndex].item = slots[toIndex].item;
        slots[fromIndex].quantity = slots[toIndex].quantity;

        slots[toIndex].item = tempItem;
        slots[toIndex].quantity = tempQty;

        // แจ้งเตือน UI ให้วาดใหม่จากข้อมูล List เดิม (ตำแหน่ง List ไม่เปลี่ยน แต่ข้อมูลข้างในเปลี่ยน)
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItemFromSlot(int index)
    {
        if (index >= 0 && index < slots.Count)
        {
            if (slots[index].IsEmpty) return;

            Debug.Log($"[Discard] ทิ้งไอเทม: {slots[index].item.itemName} จากช่อง {index}");

            // ล้างข้อมูลในช่องนั้น
            slots[index].Clear();

            // แจ้งเตือน UI ให้วาดใหม่
            OnInventoryChanged?.Invoke();
        }
    }

    public int GetTotalItemCount(string targetName)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            // ใช้ Equals พร้อม OrdinalIgnoreCase เพื่อป้องกันการพิมพ์ตัวเล็ก-ใหญ่ผิด
            if (!slot.IsEmpty && slot.item.itemName.Equals(targetName, StringComparison.OrdinalIgnoreCase))
            {
                total += slot.quantity; // บวกจำนวนจากทุกช่อง (10 + 10 = 20)
            }
        }
        return total;
    }



    public void RemoveItems(string itemName, int amountToRemove)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (amountToRemove <= 0) break;

            // เช็คชื่อแบบไม่สนตัวพิมพ์เล็ก-ใหญ่
            if (slots[i].item != null && slots[i].item.itemName.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                if (slots[i].quantity <= amountToRemove)
                {
                    amountToRemove -= slots[i].quantity;
                    slots[i].Clear();
                }
                else
                {
                    slots[i].quantity -= amountToRemove;
                    amountToRemove = 0;
                }
            }
        }
        OnInventoryChanged?.Invoke();
    }

    public void StartDragging(int index)
    {
        // เช็คว่าช่องที่จะลากมีของไหม
        if (InventoryManager.Instance.slots[index].item == null) return;

        draggingIndex = index;
        dragIcon.sprite = InventoryManager.Instance.slots[index].item.icon;
        dragIcon.gameObject.SetActive(true);
        dragIcon.rectTransform.position = Input.mousePosition;
    }

    public void UpdateDragPosition(Vector2 position)
    {
        if (draggingIndex != -1) dragIcon.rectTransform.position = position;
    }

    public void EndDragging()
    {
        draggingIndex = -1;
        dragIcon.gameObject.SetActive(false);
    }

    public void DropOnSlot(int targetIndex)
    {
        if (draggingIndex != -1 && draggingIndex != targetIndex)
        {
            InventorySlot temp = slots[draggingIndex];
            slots[draggingIndex] = slots[targetIndex];
            slots[targetIndex] = temp;

            OnInventoryChanged?.Invoke();
        }
    }

    void AddStartingItems()
    {
        foreach (ItemData item in startingItems)
        {
            
            AddItem(item, 20);
        }

        // แจ้งเตือน UI ให้วาดรูปไอเทมเริ่มต้นขึ้นมา
        OnInventoryChanged?.Invoke();
    }

    void Start()
    {
        // เรียกใช้งานหลังจาก Initialize ทุกอย่างเสร็จแล้ว
        AddStartingItems();
    }

    public int GetTotalItemCountByData(ItemData targetData)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            // เช็คว่าไฟล์ ItemData คือตัวเดียวกันเป๊ะๆ ไหม
            if (!slot.IsEmpty && slot.item == targetData)
            {
                total += slot.quantity;
            }
        }
        return total;
    }
}