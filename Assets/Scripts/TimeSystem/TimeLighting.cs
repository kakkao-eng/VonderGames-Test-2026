using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLighting : MonoBehaviour
{
    [Header("Time Colors")]
    [SerializeField] private Color morningColor = new Color(0.6f, 0.8f, 1f);
    [SerializeField] private Color afternoonColor = new Color(0.4f, 0.7f, 1f);
    [SerializeField] private Color eveningColor = new Color(0.1f, 0.1f, 0.3f);

    private Camera mainCamera;
    private Dictionary<TimePeriod, Color> colorMap;

    private void Awake()
    {
        mainCamera = Camera.main;

        colorMap = new Dictionary<TimePeriod, Color>
        {
            { TimePeriod.Morning, morningColor },
            { TimePeriod.Afternoon, afternoonColor },
            { TimePeriod.Evening, eveningColor }
        };
    }

    private void OnEnable()
    {
        TimeManager.OnTimeChanged += UpdateLighting;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeChanged -= UpdateLighting;
    }

    private void UpdateLighting(TimePeriod time, WeekDay day, int totalDay)
    {
        if (colorMap.TryGetValue(time, out Color targetColor))
        {
            mainCamera.backgroundColor = targetColor;
        }
    }
}
