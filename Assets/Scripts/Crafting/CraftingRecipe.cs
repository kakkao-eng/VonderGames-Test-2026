using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory System/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("Display Info")]
    public string recipeName;
    public Sprite recipeIcon;

    [Header("Result")]
    public ItemData resultItem;
    public int resultAmount = 1;

    [System.Serializable]
    public struct Ingredient
    {
        public ItemData item;
        public int amount;
    }

    [Header("Requirements")]
    public List<Ingredient> ingredients;
}