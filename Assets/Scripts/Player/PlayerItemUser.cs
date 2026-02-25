using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUser : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int index = InventoryUI.Instance.GetSelectedIndex();
            InventoryManager.Instance.UseItem(index);
        }
    }
}