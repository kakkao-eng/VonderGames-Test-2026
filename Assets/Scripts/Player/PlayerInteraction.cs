using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;
    public LayerMask interactableLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // เช็คว่าถ้า StoragePanel เปิดอยู่ ให้ทำการ "ปิด"
            if (StorageUI.Instance != null && StorageUI.Instance.storagePanel.activeSelf)
            {
                CloseAllUI();
            }
            else
            {
                CheckForInteractable();
            }
        }
    }

    void CloseAllUI()
    {
        StorageUI.Instance.storagePanel.SetActive(false);

        // สั่งให้ InventoryUI ปิดตัวเอง และจัดการเมาส์ให้เรียบร้อย
        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.ToggleInventory(false);
            // บรรทัดสำคัญ: คืนค่าเมาส์ให้ล็อกกลางจอเหมือนเดิม
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void CheckForInteractable()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Chest"))
            {
                StorageChest chest = hit.GetComponent<StorageChest>();
                if (chest != null)
                {
                    chest.OpenChest();
                    break;
                }
            }
        }
    }
}
