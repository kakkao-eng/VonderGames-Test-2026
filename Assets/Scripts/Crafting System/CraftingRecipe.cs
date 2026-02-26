using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory System/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("Display Info")]
    public string recipeName; // ชื่อที่จะโชว์ในเมนูคราฟต์
    public Sprite recipeIcon; // รูปไอคอน (ใช้ Mock Asset รูปสี่เหลี่ยมไปก่อน)

    [Header("Result")]
    public GameObject resultPrefab; // ตัวแปรที่ใช้เก็บ Prefab ของ Storage Chest
    public int resultAmount = 1;

    [System.Serializable]
    public struct Ingredient
    {
        public string itemName; // ชื่อวัตถุดิบ (เช่น "Lumber")
        public int amount;      // จำนวนที่ต้องใช้ (เช่น 10)
    }

    [Header("Requirements")]
    public List<Ingredient> ingredients; // รายการของที่ต้องใช้
}