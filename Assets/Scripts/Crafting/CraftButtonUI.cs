using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftButtonUI : MonoBehaviour
{
    public CraftingRecipe recipe; // ลากไฟล์ ScriptableObject มาใส่ตรงนี้
    public CraftingManager craftingManager; // ลาก CraftingManager ในฉากมาใส่

    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public Image iconImage;

    private void Start()
    {
        if (recipe != null)
        {
            nameText.text = recipe.recipeName; //
            iconImage.sprite = recipe.recipeIcon; //
        }
    }

    public void OnCraftButtonClicked()
    {
        // เลือกเอาอย่างใดอย่างหนึ่งตามความต้องการ:
        // 1. ถ้าอยากให้คราฟต์ได้เลย:
        craftingManager.CraftInstant(recipe); //

        // 2. หรือถ้าอยากให้เช็คว่าอยู่ใกล้โต๊ะไหม:
        // craftingManager.CraftAtStation(recipe); //
    }
}