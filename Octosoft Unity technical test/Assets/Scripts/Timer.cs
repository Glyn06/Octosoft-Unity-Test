using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int minutes;
    public int seconds;

    private float timer;
    [HideInInspector] public float currentSeconds;
    [HideInInspector] public int currentMinutes;

    public UnityEvent onComplete;

    private void Awake()
    {
        currentSeconds = seconds;
        currentMinutes = minutes;
    }

    private void Update()
    {
        currentSeconds -= Time.deltaTime;
        Mathf.RoundToInt(currentSeconds);

        if (currentSeconds <= 0)
        {
            currentMinutes--;
            currentSeconds = 60;
        }

        if (currentMinutes <= 0 && currentSeconds <= 0)
        {
            onComplete.Invoke();
        }
    }
}
