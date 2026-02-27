using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // ต้องมีอันนี้
using TMPro;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemIcon;
    public TMP_Text quantityText;
    public Image highlight;
    private int slotIndex;

    public bool isStorageSlot = false;

    // เก็บ Index ของตัวเองไว้
    public void SetIndex(int index) => slotIndex = index;

    public void UpdateSlot(InventorySlot slot)
    {
        if (slot.item != null)
        {
            itemIcon.sprite = slot.item.icon;
            itemIcon.enabled = true;
            quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
        }
        else
        {
            itemIcon.enabled = false;
            quantityText.text = "";
        }
    }

    // เริ่มลาก
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ถ้าช่องนี้เป็นหีบ ให้เอารูปจากหีบ ถ้าไม่ใช่ ให้เอาจากกระเป๋าตัวละคร
        Sprite iconToDrag = itemIcon.sprite;
        if (iconToDrag == null) return;

        // ส่งค่า: index, เป็นหีบไหม?, รูปไอเทม
        InventoryUI.Instance.StartDragging(slotIndex, isStorageSlot, iconToDrag);
    }

    // กำลังลาก (ให้รูปวิ่งตามเมาส์)
    public void OnDrag(PointerEventData eventData)
    {
        InventoryUI.Instance.UpdateDragPosition(eventData.position);
    }

    // ปล่อยเมาส์
    public void OnEndDrag(PointerEventData eventData)
    {
        InventoryUI.Instance.EndDragging();
    }

    public void OnDrop(PointerEventData eventData)
    {
        int fromIndex = InventoryUI.Instance.GetSelectedIndex();
        bool cameFromStorage = InventoryUI.Instance.IsDraggingFromStorage();
        int toIndex = slotIndex;

        if (fromIndex == -1) return;

        // ถ้า "ลากจากหีบ" มา "วางที่ช่องตัวละคร"
        if (cameFromStorage && !this.isStorageSlot)
        {
            // สลับของ: จากหีบ (fromIndex) เข้าตัว (toIndex)
            StorageUI.Instance.MoveItemToStorage(toIndex, fromIndex);
        }
        // ถ้า "ลากจากตัว" มา "วางที่หีบ"
        else if (!cameFromStorage && this.isStorageSlot)
        {
            StorageUI.Instance.MoveItemToStorage(fromIndex, toIndex);
        }
        // ถ้าลากในฝั่งเดียวกันเอง
        else if (!cameFromStorage && !this.isStorageSlot)
        {
            InventoryUI.Instance.DropOnSlot(toIndex);
        }
    }



    public void SetHighlight(bool active) => highlight.enabled = active;

    
}
