using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    public Timer timerRef;

    #region Singleton
    public static GameplayUIManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    #region UIManagment
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + ScoreManager.instance.Score.ToString("000");
    }
    public void UpdateTimerUI()
    {
        timerText.text = "Time" + "\n" + timerRef.currentMinutes + ":" + timerRef.currentSeconds.ToString("00");
    }

    private void Update()
    {
        UpdateTimerUI();
    }
    #endregion
}
