using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;
    private void Awake() => Instance = this;

    public bool PlaceItem(GameObject prefab)
    {
        if (prefab == null) return false;

        // คำนวณจุดที่ตัวละครยืนอยู่ (หรือจะใช้ตำแหน่งเมาส์ก็ได้)
        // ในที่นี้ขอใช้วางข้างหน้าตัวละครเล็กน้อยครับ
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;

        Vector3 spawnPos = player.transform.position + new Vector3(1, 0, 0); // วางห่างจากตัวไปทางขวา 1 หน่วย

        Instantiate(prefab, spawnPos, Quaternion.identity);
        return true; // วางสำเร็จ
    }
}