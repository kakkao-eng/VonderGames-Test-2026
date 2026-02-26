using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private InventoryManager playerInventory;

    private void Start()
    {
        // ใช้ Tag "Player" ตามที่นายตั้งไว้ใน SlimeAI
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInventory = player.GetComponent<InventoryManager>();
        }
    }

    public void TryCraft(CraftingRecipe recipe)
    {
        // 1. เช็คว่ามีของครบไหม (เช่น Lumber ครบ 10 ไหม)
        if (CanCraft(recipe))
        {
            // 2. หักวัตถุดิบออก
            foreach (var ingredient in recipe.ingredients)
            {
                playerInventory.RemoveItems(ingredient.itemName, ingredient.amount);
            }

            // 3. เพิ่มผลลัพธ์เข้ากระเป๋า (Storage Chest)
            // สมมติว่านายมีฟังก์ชัน AddItem เดิมอยู่แล้ว
            playerInventory.AddItem(recipe.resultPrefab.GetComponent<ItemData>(), recipe.resultAmount);

            Debug.Log("คราฟต์สำเร็จ!");
        }
        else
        {
            Debug.Log("วัตถุดิบไม่พอ!");
        }
    }

    private bool CanCraft(CraftingRecipe recipe)
    {
        foreach (var ingredient in recipe.ingredients)
        {
            if (playerInventory.GetTotalItemCount(ingredient.itemName) < ingredient.amount)
            {
                return false;
            }
        }
        return true;
    }
}
