using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void OnGameEnd()
    {
        Time.timeScale = 0;
    }
}
