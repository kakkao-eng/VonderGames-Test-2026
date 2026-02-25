using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("Settings")]
    public GameObject slotPrefab;
    public Transform slotParent;

    private int selectedSlotIndex = 0;

    private int firstSelectedIndex = -1;

    private List<InventorySlotUI> slotUIComponents = new List<InventorySlotUI>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateSlots();

        // Subscribe เหตุการณ์เมื่อของในกระเป๋าเปลี่ยน ให้ Update UI ทันที
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged += UpdateUI;
        }

        UpdateUI();
        UpdateHighlight();
    }

    void CreateSlots()
    {
        // ล้างช่องเก่าทิ้งก่อนเริ่ม (Modularity)
        foreach (Transform child in slotParent) Destroy(child.gameObject);
        slotUIComponents.Clear();

        for (int i = 0; i < InventoryManager.Instance.inventorySize; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();

            if (ui != null)
            {
                slotUIComponents.Add(ui);
            }
            else
            {
                Debug.LogError("Slot Prefab is missing InventorySlotUI component!");
            }
        }
    }

    // ฟังก์ชันอัปเดตหน้าตา UI ทั้งหมด
    void UpdateUI()
    {
        var slotsData = InventoryManager.Instance.slots;

        for (int i = 0; i < slotUIComponents.Count; i++)
        {
            slotUIComponents[i].SetSlot(slotsData[i], i);
        }
    }

    void Update()
    {
        // ตรวจสอบการกดปุ่ม 1-9
        for (int i = 0; i < slotUIComponents.Count; i++)
        {
            if (i < 9 && Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }

    void SelectSlot(int index)
    {
        selectedSlotIndex = index;
        UpdateHighlight();
    }

    void UpdateHighlight()
    {
        for (int i = 0; i < slotUIComponents.Count; i++)
        {
            slotUIComponents[i].SetHighlight(i == selectedSlotIndex);
        }
    }

    public int GetSelectedIndex() => selectedSlotIndex;

    public InventorySlot GetSelectedSlot()
    {
        return InventoryManager.Instance.slots[selectedSlotIndex];
    }

    public void OnSlotClicked(int index)
    {
        if (firstSelectedIndex == -1)
        {
            // คลิกครั้งแรก: เลือกช่องที่จะย้าย
            if (InventoryManager.Instance.slots[index].IsEmpty) return; // ถ้าช่องว่างไม่ต้องเลือก

            firstSelectedIndex = index;
            Debug.Log("Selected slot: " + index + " to swap.");

        }
        else
        {
            // คลิกครั้งที่สอง: สลับที่!
            InventoryManager.Instance.SwapSlots(firstSelectedIndex, index);

            Debug.Log("Swapped " + firstSelectedIndex + " with " + index);

            // รีเซ็ตค่าเพื่อเริ่มใหม่
            firstSelectedIndex = -1;
            UpdateUI();
        }
    }
}