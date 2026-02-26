using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 4;
    [SerializeField] private float lifetime = 3f; // เวลาที่ลูกพลังจะหายไปเอง

    private void Start()
    {
        Destroy(gameObject, lifetime); // ทำลายตัวเองหลังจากเวลาที่กำหนด
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // เคลื่อนที่ไปข้างหน้า
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health != null && collision.CompareTag("Enemy"))
        {
            health.TakeDamage(damage); // ทำดาเมจให้กับเป้าหมาย
            Destroy(gameObject); // ทำลายลูกพลังหลังจากชน
        }

    }
}
