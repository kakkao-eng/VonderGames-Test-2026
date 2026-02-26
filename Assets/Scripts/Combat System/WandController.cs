using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Arcane Power (AP)")]
    [SerializeField] private int maxAP = 100;
    [SerializeField] private int apCostPerShot = 10;
    private int currentAP;

    private void Awake()
    {
        ResetAP();
    }

    private void Update()
    {
        // เล็งตามเมาส์ (Optional: เพื่อความเท่)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.mousePosition);
        // ยิงเมื่อคลิกซ้าย
        if (Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }
    }
    private void TryShoot()
    {
        if (currentAP >= apCostPerShot)
        {
            Shoot();
        }
        else
        {
            Debug.Log("Out of AP!");
        }
    }

    // ตรงส่วนที่สั่งยิงใน WandController
    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectileScript = bullet.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            float lookDirection = transform.lossyScale.x > 0 ? 1f : -1f;
            projectileScript.SetDirection(new Vector2(lookDirection, 0));

            currentAP -= apCostPerShot;
            Debug.Log("Current AP: " + currentAP);
            
        }
    }

    public void ResetAP()
    {
        currentAP = maxAP;
    }
}
