using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public static event System.Action<TimePeriod, WeekDay, int> OnTimeChanged;

    public TimePeriod CurrentTime { get; private set; } = TimePeriod.Morning;
    public WeekDay CurrentWeekDay { get; private set; } = WeekDay.Monday;

    public int TotalDayCount { get; private set; } = 0;

    int totalDaysInWeek = System.Enum.GetNames(typeof(WeekDay)).Length;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AdvanceTime()
    {
        switch (CurrentTime)
        {
            case TimePeriod.Morning:
                CurrentTime = TimePeriod.Afternoon;
                break;
            case TimePeriod.Afternoon:
                CurrentTime = TimePeriod.Evening;
                break;
            case TimePeriod.Evening:
                CurrentTime = TimePeriod.Morning;
                AdvanceDay();
                break;
        }

        //Debug.Log($"Day: {TotalDayCount} | {CurrentWeekDay} | Time: {CurrentTime}");
        OnTimeChanged?.Invoke(CurrentTime, CurrentWeekDay, TotalDayCount);
    }

    private void AdvanceDay()
    {
        TotalDayCount++;
        CurrentWeekDay = (WeekDay)(((int)CurrentWeekDay + 1) % totalDaysInWeek);
    }

    private void Start()
    {
        OnTimeChanged?.Invoke(CurrentTime, CurrentWeekDay, TotalDayCount);
    }

    private void OnEnable()
    {
        // เมื่อมีการเปลี่ยนเวลา ให้เรียกฟังก์ชันรีเซ็ตค่า
        TimeManager.OnTimeChanged += HandleTimeChanged;
    }

    private void OnDisable()
    {
        // ยกเลิกการดักฟังเมื่อ Object ถูกปิดใช้งาน
        TimeManager.OnTimeChanged -= HandleTimeChanged;
    }

    private void HandleTimeChanged(TimePeriod time, WeekDay day, int dayCount)
    {
        // 1. ค้นหา Object ที่มี Tag ว่า Player ในฉาก
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // 2. สั่งรีเซ็ต HP ผ่านสคริปต์ Health ที่ตัว Player
            Health health = player.GetComponent<Health>();
            if (health != null)
            {
                health.ResetHealth();
            }

            // 3. สั่งรีเซ็ต AP ผ่านสคริปต์ WandController (ซึ่งอาจอยู่ที่ตัวลูก)
            WandController wand = player.GetComponentInChildren<WandController>();
            if (wand != null)
            {
                wand.ResetAP();
            }

            Debug.Log("Time Changed: Player stats reset via TimeManager.");
        }
    }

}
