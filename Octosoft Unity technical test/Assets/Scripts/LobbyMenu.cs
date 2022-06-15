using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerCTW networkManager;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField ipAdressInputField;
    [SerializeField] private Button joinButton;

    private void OnEnable()
    {
        NetworkManagerCTW.OnClientConnected += HandleClientConnected;
        NetworkManagerCTW.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerCTW.OnClientConnected -= HandleClientConnected;
        NetworkManagerCTW.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAdress = ipAdressInputField.text;

        networkManager.networkAddress = ipAdress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        panel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }


}
