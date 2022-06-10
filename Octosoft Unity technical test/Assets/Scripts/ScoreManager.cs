using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    #region ScoreManagment
    private int score;
    public int Score
    {
        get { return score; }
    }

    public int targetScore;
    public UnityEvent onReachTargetScore;

    public void IncreaseScore(int value)
    {
        score += value;
        GameplayUIManager.instance.UpdateScoreUI();

        if (score >= targetScore)
            onReachTargetScore.Invoke();
    }

    public void DecreaseScore(int value)
    {
        score -= value;
        GameplayUIManager.instance.UpdateScoreUI();
    }
    #endregion
}
