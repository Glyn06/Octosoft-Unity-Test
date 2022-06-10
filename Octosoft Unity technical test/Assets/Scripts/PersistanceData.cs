using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;
using System;

public class PersistanceData : MonoBehaviour
{
    #region Singleton
    public static PersistanceData instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Difficulty
    private Difficulty difficulty;
    public Difficulty defaultDifficulty;

    public void SetDifficulty(Difficulty newDifficulty)
    {
        difficulty = newDifficulty;
    }

    public Difficulty GetDifficulty()
    {
        if (difficulty != null)
            return difficulty;
        else
            return defaultDifficulty;

    }
    #endregion
}
