using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using System;

public class GamePlayer : NetworkBehaviour
{
    [SyncVar]
    public bool isLeader;
    [SyncVar]
    public string displayName = "Loading...";
    [SyncVar]
    public int score;

    public Transform spawnerTransform;

    public GameObject cameraGo;
    [SerializeField] public Camera myCam { get { return cameraGo.GetComponent<Camera>(); } }

    private NetworkManagerCTW room;
    private NetworkManagerCTW Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerCTW;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.gamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Room.gamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    [Server]
    public void SetIsLeader(bool IsLeader)
    {
        this.isLeader = IsLeader;
    }

    [Command]
    public void CmdIncreaseScore(int score)
    {
        score += score;
    }

    [Command]
    public void CmdDecreaseScore(int score)
    {
        score -= score;

        if (score < 0)
            score = 0;
    }

    private void Start()
    {
        if (displayName == "Loading...")
        {
            Destroy(gameObject);
        }

        if (isLeader)
        {
            myCam.backgroundColor = Color.green;
            myCam.rect = new Rect(0, 0, 0.5f, 1);
        }
        else
        {
            myCam.backgroundColor = new Color(255, 128, 0, 255);
            myCam.rect = new Rect(0.5f, 0, 0.5f, 1);
            Destroy(myCam.GetComponent<AudioListener>());
        }
    }
}
