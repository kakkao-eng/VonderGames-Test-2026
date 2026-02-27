using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private InventoryManager playerInventory;
    public bool isNearCraftingStation = false;

    private void Start()
    {
        
    }

    public void TryCraft(CraftingRecipe recipe)
    {
        if (CanCraft(recipe))
        {
            foreach (var ingredient in recipe.ingredients)
            {
                // แก้ไขบรรทัดนี้ด้วย: ส่ง ingredient.item เข้าไปเพื่อสั่งหักของ
                playerInventory.RemoveItems(ingredient.item.itemName, ingredient.amount);
            }

            playerInventory.AddItem(recipe.resultItem, recipe.resultAmount);
            Debug.Log("คราฟต์สำเร็จ!");
        }
        else
        {
            Debug.Log("วัตถุดิบไม่พอ!");
        }
    }

    // ฟังก์ชันสำหรับ Instant Crafting (คราฟต์ได้ตลอดเวลา)
    public void CraftInstant(CraftingRecipe recipe)
    {
        TryCraft(recipe);
    }

    // ฟังก์ชันสำหรับ Station Crafting (ต้องอยู่ใกล้โต๊ะ)
    public void CraftAtStation(CraftingRecipe recipe)
    {
        if (isNearCraftingStation)
        {
            TryCraft(recipe);
        }
        else
        {
            Debug.Log("ต้องไปที่โต๊ะคราฟต์");
        }
    }

    private bool CanCraft(CraftingRecipe recipe)
    {
        // ถ้ายังไม่มีข้อมูล Inventory ให้ลองหาใหม่เดี๋ยวนี้เลย
        if (playerInventory == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerInventory = player.GetComponent<InventoryManager>();
            }
        }

        if (recipe == null || playerInventory == null)
        {
            Debug.LogError("ยังหา Inventory ของ Player ไม่เจอ! เช็คว่าตัวละครมีสคริปต์ InventoryManager หรือยัง");
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            int count = playerInventory.GetTotalItemCount(ingredient.item.itemName);
            Debug.Log($"ต้องการ: {ingredient.item.itemName} จำนวน {ingredient.amount} | ในตัวมี: {count}");

            if (count < ingredient.amount) return false;
        }
        return true;
    }

}

