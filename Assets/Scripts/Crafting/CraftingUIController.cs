using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUIController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject craftingPanel;

    private bool isCraftingOpen = false;

    void Start()
    {
        // เริ่มเกมมาให้ปิดเมนูไว้ก่อน
        if (craftingPanel != null)
            craftingPanel.SetActive(false);
    }

    void Update()
    {
        // ตรวจสอบการกดปุ่ม C
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCraftingMenu();
        }
    }

    public void ToggleCraftingMenu()
    {
        isCraftingOpen = !isCraftingOpen;
        craftingPanel.SetActive(isCraftingOpen);

        if (isCraftingOpen)
        {
            // สั่งให้กระเป๋าปิดทันทีเมื่อเปิดหน้าต่างคราฟต์
            if (InventoryUI.Instance != null)
            {
                InventoryUI.Instance.ToggleInventory(false);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
