using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSplitter : MonoBehaviour
{
    [Header("Split Settings")]
    [SerializeField] private GameObject smallSlimePrefab;
    [SerializeField] private int splitAmount = 2;
    [SerializeField] private bool canSplit = true; // สไลม์ตัวใหญ่ติ๊กถูก ตัวเล็กไม่ต้องติ๊ก

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        // เชื่อมต่อ Event เมื่อตาย ให้เรียกฟังก์ชัน Split
        health.OnDeath.AddListener(Split);
    }

    private void Split()
    {
        if (canSplit && smallSlimePrefab != null)
        {
            for (int i = 0; i < splitAmount; i++)
            {
                // สร้างสไลม์ตัวเล็ก ณ ตำแหน่งที่ตัวใหญ่ตาย (ขยับตำแหน่งนิดหน่อยไม่ให้ซ้อนกัน)
                Vector3 spawnOffset = new Vector3(Random.Range(-0.5f, 0.5f), 0.5f, 0);
                Instantiate(smallSlimePrefab, transform.position + spawnOffset, Quaternion.identity);
            }
        }

        // ทำลายสไลม์ตัวที่ตาย
        Destroy(gameObject);
    }


}
