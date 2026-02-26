using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 4;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction = Vector2.right; // ทิศทางเริ่มต้น

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // วิ่งไปตามทิศทางที่ได้รับมอบหมาย
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null) health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}