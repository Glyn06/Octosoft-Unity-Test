using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerCTW networkManager;

    [SerializeField] private GameObject panel;

    public void HostLobby()
    {
        networkManager.StartHost();

        Debug.Log("Hosting in: " + networkManager.networkAddress);

        panel.SetActive(false);
    }
}
