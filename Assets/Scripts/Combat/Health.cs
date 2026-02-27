using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // สำหรับแจ้งเตือนระบบอื่นเมื่อเลือดลดหรือตาย

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Events")]
    public UnityEvent OnDeath; // ไว้เชื่อมกับระบบสไลม์แตกตัว หรือหน้าจอ Game Over
    public UnityEvent<int> OnHealthChanged; // ไว้เชื่อมกับ UI เลือด

    private void Awake()
    {
        ResetHealth();
    }

    // ฟังก์ชันรับดาเมจ (จะถูกเรียกจากลูกพลังหรือการชน)
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // ไม่ให้เลือดติดลบ

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ฟังก์ชันรีเซ็ตเลือด
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
        Debug.Log($"{gameObject.name} HP Reset to {currentHealth}");
    }

    private void Die()
    {
        OnDeath?.Invoke();
        // ถ้าเป็นผู้เล่นอาจจะสั่งให้รีเซ็ต แต่ถ้าเป็นศัตรูอาจจะสั่งทำลายตัวเอก
    }

    // Getter สำหรับเช็คเลือด
    public int GetCurrentHealth() => currentHealth;
}
