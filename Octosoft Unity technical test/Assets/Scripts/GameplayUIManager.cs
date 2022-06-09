using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
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

    public void UpdateUI()
    {
        scoreText.text = "Score: " + ScoreManager.instance.Score.ToString("000");
    }
    #endregion
}
