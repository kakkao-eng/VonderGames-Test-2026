using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private GameObject highlight;

    public int slotIndex;

    public void SetSlot(InventorySlot slot, int index)
    {
        slotIndex = index;
        if (slot == null || slot.IsEmpty)
        {
            iconImage.enabled = false;
            quantityText.text = "";
        }
        else
        {
            iconImage.sprite = slot.item.icon;
            iconImage.enabled = true;
            quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
        }
    }

    // ฟังก์ชันคลิกเพื่อสลับ
    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryUI.Instance.OnSlotClicked(slotIndex);
    }

    public void SetHighlight(bool isActive)
    {
        if (highlight != null) highlight.SetActive(isActive);
    }
}