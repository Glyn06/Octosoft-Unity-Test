using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void IncreaseScore(int value)
    {
        score += value;
        GameplayUIManager.instance.UpdateUI();
    }

    public void DecreaseScore(int value)
    {
        score -= value;
        GameplayUIManager.instance.UpdateUI();
    }
    #endregion
}
