using System.Collections.Generic;
using UnityEngine;

public class StorageUI : MonoBehaviour
{
    public static StorageUI Instance;
    public GameObject storagePanel;
    public Transform slotParent;

    public GameObject slotPrefab;

    private List<InventorySlotUI> storageSlots = new List<InventorySlotUI>();
    private StorageChest currentChest;

    void Awake() => Instance = this;

    void Start()
    {
        InitializeSlots();
        CreateStorageSlots();
        storagePanel.SetActive(false);
    }

    // แยกฟังก์ชันออกมาเพื่อให้เรียกซ้ำได้หากมีการเปลี่ยนแปลง Hierarchy
    public void InitializeSlots()
    {
        storageSlots.Clear();
        // ดึงเฉพาะ InventorySlotUI ที่เป็นลูกตรงๆ ของ slotParent
        foreach (InventorySlotUI ui in slotParent.GetComponentsInChildren<InventorySlotUI>(true))
        {
            storageSlots.Add(ui);
        }
        Debug.Log("StorageUI: Found " + storageSlots.Count + " slots.");
    }

    public void OpenStorage(StorageChest chest)
    {
        currentChest = chest;
        storagePanel.SetActive(true);

        // ปรับสถานะเมาส์ที่นี่โดยตรงเพื่อให้มั่นใจว่าเมาส์ไม่หาย
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // ถ้าต้องการให้เปิดกระเป๋าผู้เล่นพร้อมหีบ (เอาไว้ลากของ)
        if (InventoryUI.Instance != null)
        {
            // ใช้ฟังก์ชัน ToggleInventory(true) เพื่อเปิดแผงกระเป๋าหลัก
            InventoryUI.Instance.ToggleInventory(true);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (currentChest == null) return;

        // ถ้าจำนวนสล็อตที่หาได้ไม่ตรงกับที่หีบมี ให้ลองหาใหม่อีกรอบ
        if (storageSlots.Count == 0) InitializeSlots();

        for (int i = 0; i < storageSlots.Count; i++)
        {
            if (i < currentChest.storageSlots.Count)
            {
                storageSlots[i].UpdateSlot(currentChest.storageSlots[i]);
            }
        }
    }

    // ฟังก์ชันย้ายของข้ามระบบ (สำหรับเรียกจาก InventorySlotUI ตอน Drop)
    public void MoveItemToStorage(int playerSlotIndex, int storageSlotIndex)
    {
        if (currentChest == null) return;

        InventorySlot playerSlot = InventoryManager.Instance.slots[playerSlotIndex];
        InventorySlot chestSlot = currentChest.storageSlots[storageSlotIndex];

        // สลับข้อมูลระหว่างกระเป๋าผู้เล่นและหีบ
        InventorySlot temp = new InventorySlot(playerSlot.item, playerSlot.quantity);
        playerSlot.CopyFrom(chestSlot);
        chestSlot.CopyFrom(temp);

        RefreshUI();
        InventoryUI.Instance.UpdateUI();
    }


    void CreateStorageSlots()
    {
        // ล้างช่องเก่า (ถ้ามี)
        foreach (Transform child in slotParent) Destroy(child.gameObject);
        storageSlots.Clear();

        // สร้างช่องตามจำนวนที่ต้องการ (เช่น 30 ช่อง)
        for (int i = 0; i < 30; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            if (ui != null)
            {
                ui.SetIndex(i);
                // สำคัญ: ต้องบอกช่องนี้ด้วยว่าเป็นช่องของ Storage ไม่ใช่กระเป๋าตัวละคร
                ui.isStorageSlot = true;
                storageSlots.Add(ui);
            }
        }
    }
}