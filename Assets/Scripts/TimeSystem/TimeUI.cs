using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void OnEnable()
    {
        TimeManager.OnTimeChanged += UpdateUI;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeChanged -= UpdateUI;
    }


    private void UpdateUI(TimePeriod time, WeekDay day, int totalDay)
    {
        timeText.text =
            "Day " + totalDay +
            " - " + day +
            "\nTime: " + time;
    }
}
