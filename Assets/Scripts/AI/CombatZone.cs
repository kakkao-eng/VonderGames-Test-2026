using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        // เมื่อผู้เล่นเดินออกจากโซน
        if (collision.CompareTag("Player"))
        {
            ResetPlayerCombatState(collision.gameObject);
        }
    }

    public void ResetPlayerCombatState(GameObject player)
    {
        // สั่ง Reset HP (จาก Step 1)
        Health health = player.GetComponent<Health>();
        if (health != null) health.ResetHealth();

        // สั่ง Reset AP (จาก Step 2)
        WandController wand = player.GetComponentInChildren<WandController>();
        if (wand != null) wand.ResetAP();

        Debug.Log("Player exited combat area: Stats Reset.");
    }
}
