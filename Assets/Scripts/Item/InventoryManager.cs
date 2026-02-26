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
        if (slot.IsEmpty) return;

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

            case ItemType.Resource:
                Debug.Log($"{item.itemName} เป็นทรัพยากร ใช้โดยตรงไม่ได้");
                return; // ไม่ต้องอัปเดต UI ถ้าไม่มีการเปลี่ยนแปลง
        }

        // ล้างช่องถ้าของหมด
        if (slot.quantity <= 0) slot.Clear();

        OnInventoryChanged?.Invoke();
    }

    public void SwapSlots(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= slots.Count || indexB < 0 || indexB >= slots.Count) return;

        InventorySlot temp = slots[indexA];
        slots[indexA] = slots[indexB];
        slots[indexB] = temp;

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


    public int GetTotalItemCount(string itemName)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            if (slot.item != null && slot.item.itemName == itemName)
            {
                total += slot.quantity;
            }
        }
        return total;
    }



    public void RemoveItems(string itemName, int amountToRemove)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (amountToRemove <= 0) break;

            if (slots[i].item != null && slots[i].item.itemName == itemName)
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
}