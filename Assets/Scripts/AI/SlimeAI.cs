using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange =5f;

    [Header("Combat Settings")]
    [SerializeField] private int contactDamage = 5;

    private Transform player;

    private void Start()
    {
        // ค้นหาผู้เล่นอัตโนมัติจาก Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // ถ้าผู้เล่นอยู่ในระยะ ให้เดินเข้าหา
        if (distance <= detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบ Tag ของผู้เล่น
        if (other.CompareTag("Player"))
        {
            // ค้นหาสคริปต์ Health ที่ตัวผู้เล่น
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
                Debug.Log("Slime dealt damage to Player!");
            }
        }
    }
}
