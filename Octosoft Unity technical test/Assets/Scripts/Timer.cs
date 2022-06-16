using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;
using TMPro;

public class Timer : NetworkBehaviour
{
    public int minutes;
    public int seconds;

    [SyncVar] [SerializeField] private bool isRunning;

    [SyncVar(hook = nameof(HandleCurrentSecondsChange))] [SerializeField] private float currentSeconds;
    [SyncVar(hook = nameof(HandleCurrentMinutesChange))] [SerializeField] private int currentMinutes;

    [SerializeField] private TextMeshProUGUI timerText;

    public event Action Oncomplete;

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
                    Oncomplete?.Invoke();
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

    public void HandleCurrentSecondsChange(float oldValue, float newValue)
    {
        UpdateUI();
    }

    public void HandleCurrentMinutesChange(int oldValue, int newValue)
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        timerText.text = "Time" + "\n" + currentMinutes + ":" + currentSeconds.ToString("00");
    }

    public override void OnStartServer()
    {
        isRunning = true;
    }
}
