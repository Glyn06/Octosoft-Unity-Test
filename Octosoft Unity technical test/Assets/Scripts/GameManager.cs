using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

public enum Difficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    #region Difficulty
    private Difficulty difficulty;

    public void SetDifficulty(int newDifficulty)
    {
        switch ((Difficulty)newDifficulty)
        {
            case Difficulty.Easy:
                difficulty = Difficulty.Easy;
                break;
            case Difficulty.Normal:
                difficulty = Difficulty.Normal;
                break;
            case Difficulty.Hard:
                difficulty = Difficulty.Hard;
                break;
            default:
                difficulty = Difficulty.Normal;
                break;
        }

        Debug.Log("Difficulty set to: " + difficulty.ToString());
    }
    #endregion
    #region SceneManagement
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
    #endregion
}
