using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private List<TextMeshProUGUI> playersNameText;
    [SerializeField] private List<TextMeshProUGUI> playersReadyText;
    [SerializeField] private Button startGameButton;

    [SyncVar(hook = nameof(HandleDisplayNameChange))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChange))]
    public bool IsReady = false;


    private bool isLeader = false;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
            startGameButton.gameObject.SetActive(value);
        }
    }

    private NetworkManagerCTW room;
    private NetworkManagerCTW Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerCTW;
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayerNameInput.DisplayName);

        lobbyUI.SetActive(true);
    }

    public override void OnStartClient()
    {
        Room.roomPlayers.Add(this);

        UpdateDisplay();
        Room.NotifyPlayersOfReadyState();
    }

    public override void OnStopClient()
    {
        Room.roomPlayers.Remove(this);

        UpdateDisplay();
        Room.NotifyPlayersOfReadyState();
    }

    public void HandleDisplayNameChange(string oldValue, string newValue) => UpdateDisplay();
    public void HandleReadyStatusChange(bool oldValue, bool newValue) => UpdateDisplay();

    private void UpdateDisplay()
    {
        if (!hasAuthority)
        {
            foreach (var player in Room.roomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        for (int i = 0; i < playersNameText.Count; i++)
        {
            playersNameText[i].text = "Waiting for player...";
            playersReadyText[i].text = string.Empty;
        }

        for (int i = 0; i < Room.roomPlayers.Count; i++)
        {
            playersNameText[i].text = Room.roomPlayers[i].DisplayName;
            playersReadyText[i].text = room.roomPlayers[i].IsReady ? "<color=green>Ready</color>" : "<color=red>Not ready</color>";
        }
    }

    internal void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }

        startGameButton.interactable = readyToStart;
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }

    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;

        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if (Room.roomPlayers[0].connectionToClient != connectionToClient) { return; }
    }
}
