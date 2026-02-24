using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public static event System.Action<TimePeriod, WeekDay, int>OnTimeChanged;

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

}
