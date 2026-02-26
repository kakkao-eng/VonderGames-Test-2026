using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("UI Panels & Prefabs")]
    [Tooltip("Panel รวมที่เอาไว้ เปิด/ปิด ตอนกด Tab")]
    public GameObject fullInventoryPanel;
    [Tooltip("Prefab ของช่องเก็บของ (Slot)")]
    public GameObject slotPrefab;

    [Header("Parent Transforms")]
    [Tooltip("ที่อยู่ของช่องเก็บของ 2 แถวบน (Index 10-29)")]
    public Transform mainInventoryParent;
    [Tooltip("ที่อยู่ของช่อง Hotbar แถวล่าง (Index 0-9)")]
    public Transform hotbarParent;

    [Header("Drag & Drop Settings")]
    [Tooltip("Image ที่จะลอยตามเมาส์เวลาลาก (ต้องปิด Raycast Target ใน Inspector)")]
    public Image dragIcon;
    private int draggingIndex = -1;

    [Header("State")]
    private bool isInventoryOpen = false;
    private int selectedSlotIndex = 0;
    private List<InventorySlotUI> slotUIComponents = new List<InventorySlotUI>();
    

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 1. สร้างช่องเก็บของทั้งหมด (ทำครั้งเดียวตอนเริ่ม)
        CreateSlots();

        // 2. สมัครรับข้อมูลเมื่อ Inventory ใน Manager มีการเปลี่ยนแปลง
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged += UpdateUI;
        }

        // 3. ตั้งค่าเริ่มต้น: ปิดหน้าต่างใหญ่ และปิด Icon ลากวาง
        if (fullInventoryPanel != null) fullInventoryPanel.SetActive(false);
        if (dragIcon != null) dragIcon.gameObject.SetActive(false);

        UpdateUI();
        UpdateHighlight();
    }

    void Update()
    {
        // ระบบเปิด/ปิดกระเป๋าด้วยปุ่ม Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        // ระบบกดเลข 1-9 เพื่อเลือก Hotbar
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }

        // ระบบกดเลข 0 เพื่อเลือก Hotbar ช่องที่ 10 (Index 9)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectSlot(9);
        }

        // ถ้ากำลังลากของอยู่ ให้รูปภาพวิ่งตามเมาส์
        if (draggingIndex != -1)
        {
            UpdateDragPosition(Input.mousePosition);
        }
    }

    void CreateSlots()
    {
        // ล้างช่องเก่าทิ้งก่อน (ถ้ามี)
        foreach (Transform child in mainInventoryParent) Destroy(child.gameObject);
        foreach (Transform child in hotbarParent) Destroy(child.gameObject);
        slotUIComponents.Clear();

        // วนลูปสร้างช่องตามจำนวนที่ Manager กำหนด
        for (int i = 0; i < InventoryManager.Instance.inventorySize; i++)
        {
            // ถ้า i < 10 ลง Hotbar, ถ้ามากกว่านั้นลงแผงใหญ่
            Transform targetParent = (i < 10) ? hotbarParent : mainInventoryParent;

            GameObject go = Instantiate(slotPrefab, targetParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();

            if (ui != null)
            {
                ui.SetIndex(i); // บอกช่องว่ามันคือช่องลำดับที่เท่าไหร่
                slotUIComponents.Add(ui);
            }
        }
    }

    public void UpdateUI()
    {
        // ดึงข้อมูลล่าสุดจาก Manager มาวาดใหม่ทุกช่อง
        var slotsData = InventoryManager.Instance.slots;
        for (int i = 0; i < slotUIComponents.Count; i++)
        {
            if (i < slotsData.Count)
            {
                slotUIComponents[i].UpdateSlot(slotsData[i]);
            }
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        if (fullInventoryPanel != null) fullInventoryPanel.SetActive(isInventoryOpen);

        // จัดการเมาส์: ถ้าเปิดกระเป๋าให้เห็นเมาส์ ถ้าปิดให้ล็อกเมาส์ไว้กลางจอ
        Cursor.visible = isInventoryOpen;
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void SelectSlot(int index)
    {
        selectedSlotIndex = index;
        UpdateHighlight();
        // แจ้ง Manager ว่ากำลังใช้งานช่องนี้อยู่
        InventoryManager.Instance.UseItem(index);
    }

    void UpdateHighlight()
    {
        for (int i = 0; i < slotUIComponents.Count; i++)
        {
            slotUIComponents[i].SetHighlight(i == selectedSlotIndex);
        }
    }

    // --- ส่วนของระบบ Drag & Drop ---

    public void StartDragging(int index)
    {
        // ถ้าช่องที่พยายามลากไม่มีของ ก็ไม่ต้องทำอะไร
        if (InventoryManager.Instance.slots[index].IsEmpty) return;

        draggingIndex = index;
        dragIcon.sprite = InventoryManager.Instance.slots[index].item.icon;
        dragIcon.gameObject.SetActive(true);
    }

    public void UpdateDragPosition(Vector2 position)
    {
        if (draggingIndex != -1 && dragIcon != null)
        {
            dragIcon.transform.position = position;
        }
    }

    public void EndDragging()
    {
        draggingIndex = -1;
        if (dragIcon != null) dragIcon.gameObject.SetActive(false);
    }

    public void DropOnSlot(int targetIndex)
    {
        // ถ้ามีการลากอยู่ และวางลงในคนละช่องกับที่เริ่มลาก
        if (draggingIndex != -1 && draggingIndex != targetIndex)
        {
            // สั่งให้ Manager สลับข้อมูลใน List จริงๆ
            InventoryManager.Instance.SwapSlots(draggingIndex, targetIndex);
        }
    }

    // เพิ่มฟังก์ชันนี้ลงใน InventoryUI.cs
    public int GetSelectedIndex()
    {
        return selectedSlotIndex;
    }

}