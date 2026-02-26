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
        InventoryUI.Instance.StartDragging(slotIndex);
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

    // วางลงในช่องนี้
    public void OnDrop(PointerEventData eventData)
    {
        InventoryUI.Instance.DropOnSlot(slotIndex);
    }

    public void SetHighlight(bool active) => highlight.enabled = active;
}