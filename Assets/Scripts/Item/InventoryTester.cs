using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    public ItemData testItem;
    public int amount = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventoryManager.Instance.AddItem(testItem, amount);
            Debug.Log("Added Item");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.Instance.RemoveItem(testItem, amount);
            Debug.Log("Removed Item");
        }
       
         
    }
}