using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int minutes;
    public int seconds;

    [SerializeField] private bool isRunning;

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
        if (isRunning)
        {
            currentSeconds -= Time.deltaTime;
            Mathf.RoundToInt(currentSeconds);

            if (currentSeconds <= 0)
            {
                if (currentMinutes <= 0)
                {
                    onComplete.Invoke();
                    isRunning = false;
                }
                else
                {
                    currentMinutes--;
                    currentSeconds = 59;
                }
            }
        }
    }
}
