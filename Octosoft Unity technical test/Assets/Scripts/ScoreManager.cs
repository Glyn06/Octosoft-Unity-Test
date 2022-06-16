using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;

public class ScoreManager : NetworkBehaviour
{
    public int targetScore;

    public static event Action OnReachTargetScore;

    private NetworkManagerCTW room;
    private NetworkManagerCTW Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerCTW;
        }
    }

    public static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncreasePlayerScore(int score)
    {
        Room.HandleIncreaseScore(score);
    }

    public void DecreasePlayerScore(int score)
    {
        Room.HandleDecreaseScore(score);
    }
}
